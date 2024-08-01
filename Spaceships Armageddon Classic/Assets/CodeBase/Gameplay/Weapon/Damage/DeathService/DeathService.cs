using System;
using CodeBase.Services.UnitRegistry;
using CodeBase.Units;

namespace CodeBase.Weapon.Damage.DeathService
{
    public delegate void Killed(Guid targetId, Guid attackerId);
    
    public class DeathService : IDeathService
    {
        public event Killed Killed;
        
        private readonly IUnitRegistry _unitRegistry;

        public DeathService(IUnitRegistry unitRegistry)
        {
            _unitRegistry = unitRegistry;
        }

        public void Kill(IUnitBehaviour attacker, IUnitBehaviour target)
        {
            Killed?.Invoke(target.Id, attacker.Id);
            target.Transform.gameObject.SetActive(false);
            _unitRegistry.Unregister(target.Id);
        }
    }
}