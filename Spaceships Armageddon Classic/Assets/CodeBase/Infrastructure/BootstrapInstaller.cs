using CodeBase.Enemy.Loot;
using CodeBase.Factory;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.DiExtension;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Loot;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Modules.WeaponModule;
using CodeBase.Services.BattleExperience;
using CodeBase.Services.DistanceCalculator;
using CodeBase.Services.UnitRegistry;
using CodeBase.UI;
using CodeBase.Weapon.Damage.Appliers;
using CodeBase.Weapon.Damage.DeathService;
using CodeBase.Weapon.Projectile;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Curtain _curtain;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.BindInterfacesTo<BattleTextInformer>().AsSingle();
            Container.BindInterfacesTo<DistanceCalculator>().AsSingle();

            Container.Bind<Curtain>().FromComponentInNewPrefab(_curtain).AsSingle();

            Container.RegisterService<IAssetProvider, AssetProvider>().NonLazy();
            Container.RegisterService<ISaveLoadService, SaveLoadService>().NonLazy();
            Container.RegisterService<IBattleExperience, BattleExperience>().NonLazy();
            
            Container.RegisterService<IInputService, InputService>();
            Container.RegisterService<ISceneLoader, SceneLoader>();
            Container.RegisterService<IPersistentProgressService, PersistentProgressService>();
            Container.RegisterService<IUnitRegistry, UnitRegistry>();
            Container.RegisterService<IStaticDataService, StaticDataService>();
            Container.RegisterService<IUnitFactory, UnitFactory>();
            Container.RegisterService<IHudFactory, HudFactory>();
            Container.RegisterService<IGameFactory, GameFactory>();
            Container.RegisterService<IDamageApplier, DamageApplier>();
            Container.RegisterService<IDeathService, DeathService>();
            Container.RegisterService<ILootFactory, LootFactory>();
            Container.RegisterService<ILootSpawner, LootSpawner>().NonLazy();
            Container.RegisterService<IPickLootService, PickLootService>();

            Container.RegisterService<IProjectileFactory<Bullet>, BulletFactory>();
            Container.RegisterService<IProjectileFactory<Beam>, BeamFactory>();

            Container.Bind<GameStateMachine>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IStaticDataService>().Load();
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();
        }
    }
}