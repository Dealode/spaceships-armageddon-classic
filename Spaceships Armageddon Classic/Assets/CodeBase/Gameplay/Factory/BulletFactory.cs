using CodeBase.Modules.WeaponModule;
using CodeBase.Units;
using CodeBase.Weapon.Damage.Appliers;
using CodeBase.Weapon.Projectile;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class BulletFactory : IProjectileFactory<Bullet>
    {
        private IInstantiator _instantiator;
        private IDamageApplier _damageApplier;

        public BulletFactory(IInstantiator instantiator, IDamageApplier damageApplier)
        {
            _damageApplier = damageApplier;
            _instantiator = instantiator;
        }

        public Bullet Create(UnitBehaviour attacker, WeaponModule weapon, Transform targetTransform)
        {
            var direction = (targetTransform.position - attacker.Transform.position).normalized;
            
            var projectile = _instantiator.InstantiatePrefabForComponent<Bullet>(weapon.Projectile, attacker.Transform.position, Quaternion.LookRotation(direction), null);
            
            //todo projectile.Initialize(_damageApplier, weapon, attacker);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * weapon.ProjectileSpeed, ForceMode.VelocityChange);

            projectile.Damage = weapon.damage;
            
            return projectile;
        }
    }
}