using System;
using CodeBase.Modules.WeaponModule;
using CodeBase.Services.UnitRegistry;
using CodeBase.UI;
using CodeBase.Units;
using CodeBase.Weapon.Damage.DeathService;
using UnityEngine;

namespace CodeBase.Weapon.Damage.Appliers
{
    public class DamageApplier : IDamageApplier
    {
        private readonly IUnitRegistry _unitRegistry;
        private readonly IBattleTextInformer _battleTextInformer;
        private readonly IDeathService _deathService;

        public DamageApplier(IUnitRegistry unitRegistry, IBattleTextInformer battleTextInformer, IDeathService deathService)
        {
            _deathService = deathService;
            _battleTextInformer = battleTextInformer;
            _unitRegistry = unitRegistry;
        }

        public void Apply(WeaponModule weaponModule, float damage, IUnitBehaviour target, IUnitBehaviour attacker)
        {
            if (!_unitRegistry.IsAlive(target.Id))
                return;
            
            Debug.Log($"Deal {damage} damage weapon {weaponModule.id}");

            if (target.State.CurrentShield.Value > 0)
            {
                ProcessDamage(ref damage, target.State.CurrentShield.Value, weaponModule.shieldMultiply, weaponModule.ignoreShield, target.State.AddShield, Color.cyan, target.Transform);
            }

            target.State.CurrentHull.Value -= damage;
            
            if (target.State.CurrentHull.Value > 0) return;
            
            target.State.CurrentHull.Value = 0;
            _deathService.Kill(attacker, target);
        }
        
        private void ProcessDamage(ref float damage, float protection, float multiplier, float ignoreProtection, Action<float> damageAction, Color color, Transform targetTransform)
        {
            if (!(protection > 0)) return;
            if (damage <= 0) return;
            
            var damageToProtection = CalculationDamageTo(ref damage, protection, multiplier, ignoreProtection);
            damageAction(-damageToProtection);
        }

        private static float CalculationDamageTo(ref float damage, float protectionHealth, float multiplyDamage, float ignoreProtection)
        {
            var damageToProtection = DamageTo(protectionHealth / (1 - multiplyDamage), damage * (1 - ignoreProtection));
            damage -= damageToProtection;
            damageToProtection *= multiplyDamage;

            return damageToProtection;
        }

        private static float DamageTo(float value, float damage) => 
            value > damage ? damage : value;
    }
}