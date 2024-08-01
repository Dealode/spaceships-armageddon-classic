using CodeBase.Modules.WeaponModule;
using CodeBase.Units;
using CodeBase.Weapon.Projectile;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface IProjectileFactory<T> where T : Projectile
    {
        T Create(UnitBehaviour attacker, WeaponModule weapon, Transform targetTransform);
    }
}