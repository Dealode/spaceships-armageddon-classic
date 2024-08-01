using CodeBase.Enemy;
using CodeBase.Factory;
using CodeBase.Player;
using CodeBase.Units;
using CodeBase.Weapon.Damage.Appliers;
using CodeBase.Weapon.Projectile;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Modules.WeaponModule
{
    public class ProjectileShootStrategy : IShootStrategy
    {
        private readonly IProjectileFactory<Bullet> _bulletFactory;
        private IDamageApplier _damageApplier;

        public ProjectileShootStrategy(IProjectileFactory<Bullet> bulletFactory, IDamageApplier damageApplier)
        {
            _damageApplier = damageApplier;
            _bulletFactory = bulletFactory;
        }

        public async UniTask Shoot(UnitBehaviour unit, WeaponModule weapon, UnitBehaviour target)
        {
            var bullet = _bulletFactory.Create(unit, weapon, target.Transform);

            if (unit is PlayerBehaviour)
            {
                bullet.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
            }
            else if(unit is EnemyBehaviour)
            {
                bullet.gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
            }
            
            bullet.OnHit = (injured) =>
            {
                _damageApplier.Apply(weapon, bullet.Damage, injured, unit);
                Object.Destroy(bullet.gameObject);
            };
        }
    }
}