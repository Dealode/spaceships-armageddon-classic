using System;
using UnityEngine;

namespace CodeBase.Weapon.FirePoints
{
    public class FirePoint : MonoBehaviour
    {
        public Guid firePointGuid;
        
        public virtual Vector3 GetPoint()
        {
            return transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f);
            Gizmos.DrawSphere(transform.position, 0.7f);
            Gizmos.color = Color.white;
        }
    }
}