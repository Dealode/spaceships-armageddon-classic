using CodeBase.Units;

namespace CodeBase.Weapon.Damage.DeathService
{
    public interface IDeathService
    {
        public void Kill(IUnitBehaviour attacker, IUnitBehaviour target);
        event Killed Killed;
    }
}