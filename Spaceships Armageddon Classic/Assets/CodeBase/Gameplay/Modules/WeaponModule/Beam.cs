using CodeBase.Weapon.Projectile;
using UnityEngine;

namespace CodeBase.Modules.WeaponModule
{
    public class Beam : Projectile
    {
        [SerializeField] private LineRenderer lineRenderer;

        private Transform _attacker;
        private Transform _target;
        
        public void Setup(Transform source, Transform destination)
        {
            _attacker = source;
            _target = destination;
        }
        
        private void Update()
        {
            if (_attacker == null || _target == null)
                return;
            
            UpdateLaserPosition(_attacker.position, _target.position);
        }

        private void UpdateLaserPosition(Vector3 from, Vector3 to)
        {
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);
        }
        
        
    }
}