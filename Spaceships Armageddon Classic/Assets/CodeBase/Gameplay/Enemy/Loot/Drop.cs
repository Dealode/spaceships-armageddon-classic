using System;
using CodeBase.StaticData.Loot;

namespace CodeBase.Enemy.Loot
{
    [Serializable]
    public struct Drop
    {
        public LootTypeId LootTypeId;
        public int Chance;
    }
}