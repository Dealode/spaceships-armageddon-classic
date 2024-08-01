using System.Collections.Generic;
using CodeBase.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Weapon.FirePoints
{
    public class UnitFirePoints : MonoBehaviour
    {
        [FormerlySerializedAs("_firePoint")] [SerializeField] private FirePoint[] _firePoints;
        
        private Dictionary<WeaponSlot, FirePoint> _linkedWeaponToFirePoints;

        private int _nextRegisterSlot;
        
        public void Setup()
        {
            _linkedWeaponToFirePoints = new Dictionary<WeaponSlot, FirePoint>(_firePoints.Length);
            _nextRegisterSlot = 0;
        }

        public void RegisterSlot(WeaponSlot weaponSlot)
        {
            _linkedWeaponToFirePoints.TryAdd(weaponSlot, GetFirePoint());
        }

        private FirePoint GetFirePoint()
        {
            var fireslot = _firePoints[_nextRegisterSlot];
            
            if (++_nextRegisterSlot > _firePoints.Length) _nextRegisterSlot = 0;

            return fireslot;
        }
    }
}