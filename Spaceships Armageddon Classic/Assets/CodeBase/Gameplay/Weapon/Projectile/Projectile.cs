using System;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.Weapon.Projectile
{
    public class Projectile : MonoBehaviour
    {
        public float Damage { get; set; }
        
        public Action<UnitBehaviour> OnHit { set; get; }
    }
}