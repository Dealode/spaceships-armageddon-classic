using CodeBase.Modules.Module;
using CodeBase.StaticData.ColorsRegistry;
using CodeBase.Weapon.Projectile;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.StaticData.Weapons
{
    [CreateAssetMenu(menuName = "StaticData/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        public WeaponTypeId WeaponTypeId;
        public string Name;
        public DimensionType dimensionType;
        public RarityType Rarity;
        public ShootStrategyType ShootStrategy;
        
        public float Damage;
        public int Range;
        public float Cooldown;
        /// <summary>
        /// Точність на 100м
        /// </summary>
        [Range(0, 1)] public float Accuracy;
        public float ProjectileSpeed;
        
        [FormerlySerializedAs("Shield")]
        [Space]
        public int ShieldMultiplier;
        [FormerlySerializedAs("Hull")]
        public int HealthMultiplier;
        [Range(0,100)]
        public int IgnoreShield;
        
        public Projectile _Projectile;
    }
}