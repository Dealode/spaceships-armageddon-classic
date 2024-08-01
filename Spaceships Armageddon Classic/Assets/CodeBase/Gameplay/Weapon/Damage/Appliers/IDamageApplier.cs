using CodeBase.Modules.WeaponModule;
using CodeBase.Units;

namespace CodeBase.Weapon.Damage.Appliers
{
    public interface IDamageApplier
    {
        void Apply(WeaponModule weaponModule, float damage, IUnitBehaviour target, IUnitBehaviour attacker);
    }
}