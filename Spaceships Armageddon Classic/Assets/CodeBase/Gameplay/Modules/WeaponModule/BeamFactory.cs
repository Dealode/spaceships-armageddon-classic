using CodeBase.Factory;
using CodeBase.Units;
using UnityEngine;
using Zenject;

namespace CodeBase.Modules.WeaponModule
{
    public class BeamFactory : IProjectileFactory<Beam>
    {
        private readonly IInstantiator _instantiator;

        public BeamFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
            
         public Beam Create(UnitBehaviour attacker, WeaponModule weapon, Transform targetTransform)
        {
            var direction = (targetTransform.position - attacker.Transform.position).normalized;
            
            var beam = _instantiator.InstantiatePrefabForComponent<Beam>(weapon.Projectile, attacker.Transform.position, Quaternion.LookRotation(direction), null);
            beam.Damage = weapon.damage;
            return beam;
        }
    }
}