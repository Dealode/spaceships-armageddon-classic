using CodeBase.Factory;
using CodeBase.Player;
using CodeBase.Weapon;
using UnityEngine;
using Zenject;

namespace CodeBase.Units
{
    public class ShootingSystem : MonoBehaviour, IUnitBehaviourNeed, IUnitWeaponNeed
    {
        private UnitBehaviour _unit;
        private UnitWeapons _unitWeapons;
        
        void IUnitBehaviourNeed.Setup(UnitBehaviour unitBehaviour) => 
            _unit = unitBehaviour;

        void IUnitWeaponNeed.Setup(UnitWeapons unitWeapons) => 
            _unitWeapons = unitWeapons;

        public void Shoot(UnitBehaviour target, float distance)
        {
            if (target == null)
                return;
            
            for (var i = 0; i < _unitWeapons.WeaponsSlots.Length; i++)
            {
                var weapon = _unitWeapons.WeaponsSlots[i].WeaponModule;

                if (weapon == null)
                    continue;

                if (!weapon.IsReady)
                    continue;
                
                if (weapon.range < distance)
                    continue;
                
                if (target == null)
                    continue;
                
                weapon.ShootStrategy.Shoot(_unit as PlayerBehaviour, weapon, target);
                weapon.StartCooldown();
            }
        }
    }
}