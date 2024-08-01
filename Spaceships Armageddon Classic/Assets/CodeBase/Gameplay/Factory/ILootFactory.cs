using CodeBase.StaticData.Loot;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface ILootFactory
    {
        void CreateLootAt(LootTypeId lootKey, Vector3 transformPosition);
    }
}