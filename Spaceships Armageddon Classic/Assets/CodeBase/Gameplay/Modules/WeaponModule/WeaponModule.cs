using System;
using CodeBase.Extension;
using CodeBase.Modules.Module;
using CodeBase.StaticData.ColorsRegistry;
using CodeBase.StaticData.Units;
using CodeBase.Weapon.Projectile;
using Object = UnityEngine.Object;

namespace CodeBase.Modules.WeaponModule
{
    [Serializable]
    public class WeaponModule : IModule
    {
        public Guid id;
        public WeaponTypeId weaponTypeId { get; set; }
        public DimensionType dimensionType { get; set; }
        public RarityType Rarity { get; set; }
        public string Name { get; set; }
        public float shieldMultiply;
        public float ignoreShield;
        public float hullMultiply;
        public float damage;
        public int range;
        public float accuracy;
        public float maxCooldown;
        public float cooldown;
        public bool IsReady => cooldown <= 0;
        public Projectile Projectile { get; set; }
        public float ProjectileSpeed { get; set; }

        public void StartCooldown() => cooldown = maxCooldown;

        public IShootStrategy ShootStrategy { get; private set; }

        public void TickCooldown(float deltaTime)
        {
            if (cooldown > 0)
                cooldown -= deltaTime;
        }
        
        public void SetShootStrategy(IShootStrategy shootStrategy) =>
            ShootStrategy = shootStrategy;

        public static WeaponModule FromUnitWeaponSlot(UnitWeaponSlot arg)
        {
            var weaponConfig = arg.WeaponConfig;

            return new WeaponModule()
                .With(x => x.id = Guid.NewGuid())
                .With(x => x.dimensionType = arg.dimensionType)
                .With(x => x.weaponTypeId = arg.WeaponConfig.WeaponTypeId)
                .With(x => x.Rarity = weaponConfig.Rarity)
                .With(x => x.shieldMultiply = (float) arg.WeaponConfig.ShieldMultiplier / 100)
                .With(x => x.ignoreShield = (float) arg.WeaponConfig.IgnoreShield / 100)
                .With(x => x.hullMultiply = (float) arg.WeaponConfig.HealthMultiplier / 100)
                .With(x => x.damage = weaponConfig.Damage)
                .With(x => x.range = weaponConfig.Range)
                .With(x => x.cooldown = x.maxCooldown = weaponConfig.Cooldown)
                .With(x => x.accuracy = weaponConfig.Accuracy)
                .With(x => x.Projectile = arg.WeaponConfig._Projectile)
                .With(x => x.ProjectileSpeed = weaponConfig.ProjectileSpeed);
        }
    }
}