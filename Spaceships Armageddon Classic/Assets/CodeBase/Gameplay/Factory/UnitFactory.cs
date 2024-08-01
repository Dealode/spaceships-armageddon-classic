using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemy;
using CodeBase.Extension;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Modules.WeaponModule;
using CodeBase.Player;
using CodeBase.StaticData.Units;
using CodeBase.StaticData.Weapons;
using CodeBase.Units;
using CodeBase.Weapon;
using CodeBase.Weapon.Damage;
using CodeBase.Weapon.Damage.Appliers;
using CodeBase.Weapon.Projectile;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class UnitFactory : IUnitFactory
    {
        private readonly int _playerLayer = LayerMask.NameToLayer("Player");
        private readonly int _enemyLayer = LayerMask.NameToLayer("Enemy");
        
        private readonly IStaticDataService _staticDataService;
        private readonly IInstantiator _instantiator;
        private readonly DiContainer _diContainer;
        
        public List<UnitBehaviour> Units { get; } = new();
        
        public UnitFactory(IInstantiator instantiator, DiContainer diContainer)
        {
            _instantiator = instantiator;
            _diContainer = diContainer;
            _staticDataService = diContainer.Resolve<IStaticDataService>();
        }
        
        public void CleanUp() => 
            Units.Clear();

        public PlayerBehaviour CreatePlayerAt(in UnitTypeId unitTypeId, in Vector3 position, Transform parent = null)
        {
            var unit = CreateUnitAt<PlayerBehaviour>(unitTypeId, position, parent);
            unit.gameObject.layer = _playerLayer;
            
            _instantiator.InstantiatePrefabResource("Prefabs/Player/PlayerBehaviour", unit.transform);

            return unit;
        }

        public EnemyBehaviour CreateEnemyAt(in UnitTypeId unitTypeId, in Vector3 position, Transform parent = null)
        {
            var enemy = CreateUnitAt<EnemyBehaviour>(unitTypeId, position, parent);
            enemy.gameObject.layer = _enemyLayer;
            
            enemy.DropsOneOf = _staticDataService.UnitConfigFor(unitTypeId).Drops;

            enemy.gameObject.AddComponent<ContactDamage>();
            
            return enemy;
        }

        private T CreateUnitAt<T>(in UnitTypeId unitTypeId, in Vector3 position, Transform parent = null) where T : UnitBehaviour
        {
            var playerUnitConfig = _staticDataService.UnitConfigFor(unitTypeId);
            var go = _instantiator.InstantiatePrefab(playerUnitConfig.Prefab, position, Quaternion.identity, parent);
            var unit = go.AddComponent<T>();

            var unitWeapons = go.GetComponent<UnitWeapons>();
            
            if (unitWeapons != null)
            {
                unitWeapons.SetupSlots(SetupWeapons(playerUnitConfig.WeaponSlots));
                
                var needWeapon = unitWeapons.GetComponents<IUnitWeaponNeed>();
            
                for (var i = 0; i < needWeapon.Length; i++) 
                    needWeapon[i].Setup(unitWeapons);
            }
            
            unit.GetComponent<ReloadWeapons>().Initialize(unitWeapons);
            
            unit.TypeId = unitTypeId;
            unit.Id = Guid.NewGuid();

            var state = new UnitState()
                .With(x => x.CurrentShield.Value = x.MaxShield = playerUnitConfig.shield)
                .With(x => x.CurrentHull.Value = x.MaxHull = playerUnitConfig.hull)
                .With(x => x.RotationSpeed = playerUnitConfig.RotationSpeed)
                .With(x => x.Speed = playerUnitConfig.Speed)
                .With(x => x.Acceleration = playerUnitConfig.Acceleration);

            unit.InitializeWithState(state);
            unit.GetComponent<UnitMove>().InitializeWithState(state);

            var need = unit.GetComponents<IUnitBehaviourNeed>();

            if (need != null)
            {
                for (var i = 0; i < need.Length; i++) 
                    need[i].Setup(unit);
            }

            Units.Add(unit);
            return unit;
        }

        private WeaponSlot[] SetupWeapons(List<UnitWeaponSlot> weaponSlots)
        {
            var buffer = new WeaponSlot[weaponSlots.Count];

            for (int i = 0; i < weaponSlots.Count; i++)
            {
                buffer[i] = new WeaponSlot
                {
                    ID = Guid.NewGuid(),
                    SlotType = weaponSlots[i].dimensionType,
                    WeaponModule = null
                };
                
                if (weaponSlots[i].WeaponConfig == null)
                    continue;

                buffer[i].WeaponModule = WeaponModule.FromUnitWeaponSlot(weaponSlots[i]);
                buffer[i].WeaponModule
                    .SetShootStrategy(PickShootStrategy(weaponSlots[i].WeaponConfig.ShootStrategy, _diContainer));
            }
            
            return buffer;
        }

        private static IShootStrategy PickShootStrategy(ShootStrategyType weaponConfigShootStrategy, DiContainer diContainer)
        {
            return weaponConfigShootStrategy switch
            {
                ShootStrategyType.Beam => CreateBeamShootStrategy(diContainer),
                ShootStrategyType.Projectile => CreateProjectileShootStrategy(diContainer),
                
                _ => throw new ArgumentOutOfRangeException(nameof(weaponConfigShootStrategy), weaponConfigShootStrategy,
                    null)
            };
        }

        private static ProjectileShootStrategy CreateProjectileShootStrategy(DiContainer diContainer) => 
            new(diContainer.Resolve<IProjectileFactory<Bullet>>(), diContainer.Resolve<IDamageApplier>());

        private static BeamShootStrategy CreateBeamShootStrategy(DiContainer diContainer) => 
            new(diContainer.Resolve<IDamageApplier>(), diContainer.Resolve<IProjectileFactory<Beam>>());
    }
}