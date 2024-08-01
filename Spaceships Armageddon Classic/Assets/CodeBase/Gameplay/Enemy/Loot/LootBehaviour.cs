using CodeBase.StaticData.Loot;
using UnityEngine;

namespace CodeBase.Enemy.Loot
{
    public class LootBehaviour : MonoBehaviour
    {
        public LootTypeId LootType { private set; get; }
        
        public void Initialize(LootTypeId lootType) =>
            LootType = lootType;
    }
}