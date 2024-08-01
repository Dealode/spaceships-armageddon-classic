using System.Collections.Generic;
using CodeBase.Enemy.Loot;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerLootMagnet : MonoBehaviour
    {
        private const float StartStrength = 5f;
        private const float Acceleration = 0.5f;
        
        private List<CapturedLoot> CapturedLoots { get; } = new List<CapturedLoot>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LootBehaviour loot))
                CapturedLoots.Add(new CapturedLoot(loot, StartStrength));
        }

        private void Update()
        {
            for (var i = 0; i < CapturedLoots.Count; i++)
            {
                var capturedLoot = CapturedLoots[i];
                capturedLoot.Loot.transform.position = MagnetPosition(capturedLoot);
                capturedLoot.Strength += Acceleration;
            }
        }

        private Vector3 MagnetPosition(in CapturedLoot capturedLoot)
        {
            return Vector3.Slerp(capturedLoot.Loot.transform.position,
                transform.position, capturedLoot.Strength * Time.deltaTime);
        }

        private struct CapturedLoot
        {
            public LootBehaviour Loot { get; }
            public float Strength { get; set; }

            public CapturedLoot(LootBehaviour loot, float strength)
            {
                Loot = loot;
                Strength = strength;
            }
        }
    }
}