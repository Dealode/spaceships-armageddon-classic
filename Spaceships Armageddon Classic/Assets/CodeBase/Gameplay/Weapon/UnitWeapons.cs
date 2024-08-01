using CodeBase.Modules.WeaponModule;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.Weapon
{
    public class UnitWeapons : MonoBehaviour
    {
        public WeaponSlot[] WeaponsSlots;

        public float MaxRange { private set; get; }
    
        public void SetupSlots(WeaponSlot[] weaponSlots)
        {
            WeaponsSlots = weaponSlots;
        }

        public bool AddWeaponToSlot(WeaponModule weapon, int slotId)
        {
            if (slotId < 0 || slotId >= WeaponsSlots.Length)
                return false;
        
            WeaponsSlots[slotId].WeaponModule = weapon;
        
            MaxRange = weapon.range > MaxRange
                ? weapon.range
                : MaxRange;

            return true;
        }

        public bool RemoveFromSlot(int slotId)
        {
            if (slotId < 0 || slotId >= WeaponsSlots.Length)
                return false;
        
            WeaponsSlots[slotId].WeaponModule = null;

            MaxRange = CalcMaxRange();
        
            return true;
        }
    
        public bool RemoveFromSlot(WeaponModule weapon)
        {
            for (int i = 0; i < WeaponsSlots.Length; i++)
            {
                if (WeaponsSlots[i].WeaponModule == weapon)
                {
                    WeaponsSlots[i].WeaponModule = null;
                    MaxRange = CalcMaxRange();
                    return true;
                }
            }

            return false;
        }

        public bool ReplaceWithNewOne(WeaponModule newWeapon, int slotId)
        {
            if (slotId < 0 || slotId >= WeaponsSlots.Length)
                return false;
        
            WeaponsSlots[slotId].WeaponModule = newWeapon;

            MaxRange = CalcMaxRange();

            return true;
        }
    
        public bool ReplaceWithNewOne(WeaponModule oldWeapon, WeaponModule newWeapon)
        {
            for (int i = 0; i < WeaponsSlots.Length; i++)
            {
                if (WeaponsSlots[i].WeaponModule == oldWeapon)
                {
                    WeaponsSlots[i].WeaponModule = newWeapon;
                    MaxRange = CalcMaxRange();
                    return true;
                }
            }

            return false;
        }

        private float CalcMaxRange()
        {
            var maxRange = 0;
        
            for (int i = 0; i < WeaponsSlots.Length; i++)
            {
                if (WeaponsSlots[i].WeaponModule == null)
                    continue;
            
                maxRange = WeaponsSlots[i].WeaponModule.range > maxRange
                    ? WeaponsSlots[i].WeaponModule.range
                    : maxRange;
            }
        
            return maxRange;
        }
    }
}
