using CodeBase.Factory;
using CodeBase.Units;
using CodeBase.Weapon.Damage.Appliers;
using Cysharp.Threading.Tasks;

namespace CodeBase.Modules.WeaponModule
{
    public class BeamShootStrategy : IShootStrategy
    {
        private readonly IDamageApplier _damageApplier;
        private readonly IProjectileFactory<Beam> _factory;

        public BeamShootStrategy(IDamageApplier damageApplier, IProjectileFactory<Beam> factory)
        {
            _damageApplier = damageApplier;
            _factory = factory;
        }

        public async UniTask Shoot(UnitBehaviour unit, WeaponModule weapon, UnitBehaviour target)
        {
            var beam =_factory.Create(unit, weapon, target.Transform);
            beam.Setup(unit.Transform, target.Transform);
            
            _damageApplier.Apply(weapon, beam.Damage, target, unit);
            
            await UniTask.Delay(300);
            
            beam.gameObject.SetActive(false);
        }
    }
}