using CodeBase.Research;
using CodeBase.StaticData.ColorsRegistry;
using CodeBase.StaticData.Loot;
using CodeBase.StaticData.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LootConfig LootConfigFor(in LootTypeId typeId);
        UnitConfig UnitConfigFor(in UnitTypeId typeId);
        Color32 ColorByRarity(in RarityType rarity);
        LevelStaticData ForLevel(string sceneKey);
    }
}