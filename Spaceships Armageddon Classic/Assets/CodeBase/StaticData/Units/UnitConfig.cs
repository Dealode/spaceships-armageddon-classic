using System.Collections.Generic;
using CodeBase.Enemy.Loot;
using CodeBase.Modules.Module;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData.Units
{
    [CreateAssetMenu(menuName = "StaticData/UnitConfig")]
    public class UnitConfig : ScriptableObject
    {
        public string Name;
        public UnitTypeId TypeId;
        [FormerlySerializedAs("Dimension")] public DimensionType dimensionType;
        
        public List<UnitWeaponSlot> WeaponSlots = new();
        
        public float hull;
        public float armor;
        public float shield;
        
        public float EvadeChance;
        public float RotationSpeed;
        public float Speed;

        public float Acceleration;

        public List<DropOneOf> Drops;

        public GameObject Prefab;

        private void OnValidate()
        {
            for (var i = 0; i < WeaponSlots.Count; i++)
            {
                var weaponSlot = WeaponSlots[i];
                if (weaponSlot == null)
                    continue;

                if (weaponSlot.WeaponConfig == null)
                    continue;

                if (weaponSlot.WeaponConfig.dimensionType == weaponSlot.dimensionType) continue;

                Debug.LogError($"Weapon {weaponSlot.WeaponConfig.dimensionType} doesn't have slot {weaponSlot.dimensionType}");
                weaponSlot.WeaponConfig = null;
            }
        }
    }
}