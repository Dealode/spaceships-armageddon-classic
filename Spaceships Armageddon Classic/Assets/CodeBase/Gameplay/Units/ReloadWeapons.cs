using CodeBase.Weapon;
using UnityEngine;

namespace CodeBase.Units
{
    public class ReloadWeapons : MonoBehaviour
    {
        private UnitWeapons _unitWeapons;

        public void Initialize(UnitWeapons unitBehaviour)
        {
            _unitWeapons = unitBehaviour;
        }

        private void Update()
        {
            if (_unitWeapons == null)
                return;

            if (_unitWeapons.WeaponsSlots == null)
                return;
            
            for (var i = 0; i < _unitWeapons.WeaponsSlots.Length; i++)
            {
                if (_unitWeapons.WeaponsSlots[i].WeaponModule == null)
                    continue;
                
                _unitWeapons.WeaponsSlots[i].WeaponModule.TickCooldown(Time.deltaTime);
            }
        }
    }
}