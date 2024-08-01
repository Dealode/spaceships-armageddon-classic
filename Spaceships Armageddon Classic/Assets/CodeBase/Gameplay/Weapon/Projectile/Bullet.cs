using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Weapon.Projectile
{
    public class Bullet : Projectile
    {
        [SerializeField] private Collider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<EnemyBehaviour>(out var enemyBehaviour))
            {
                OnHit?.Invoke(enemyBehaviour);
            }
        }
    }
}
