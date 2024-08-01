using CodeBase.Enemy.Loot;
using UnityEngine;

namespace CodeBase.StaticData.Loot
{
    [CreateAssetMenu(fileName = "LootConfig", menuName = "StaticData/LootConfig", order = 1)]
    public class LootConfig : ScriptableObject
    {
        public LootTypeId TypeId;
        public int Value;
        public int ValuePercent;
        public LootBehaviour lootBehaviours;
    }
}