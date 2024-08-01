using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Research;
using CodeBase.StaticData.ColorsRegistry;
using CodeBase.StaticData.Loot;
using CodeBase.StaticData.Units;
using CodeBase.StaticData.Weapons;
using UnityEngine;
using WeaponTypeId = CodeBase.Modules.Module.WeaponTypeId;

namespace CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WeaponConfigPath = "Configs/WeaponsSlots";
        private const string UnitConfigPath = "Configs/Units";
        private const string LootConfigPath = "Configs/Drops";
        private const string ColorConfigPath = "Configs/Colors";

        private Dictionary<WeaponTypeId, WeaponConfig> _weaponConfigs;
        private Dictionary<UnitTypeId, UnitConfig> _unitConfigs;
        private Dictionary<LootTypeId, LootConfig> _lootConfigs;
        private Dictionary<RarityType, Color32> _rarityConfigs;
        private Dictionary<string, LevelStaticData> _levelConfigs;
        private ResearchTree _researchTree;

        public void Load()
        {
            LoadColorConfig();
            LoadWeaponConfig();
            LoadUnitConfig();
            LoadLootConfig();
            LoadLevelConfig();
        }
        
        private void LoadColorConfig()
        {
            var list = Resources.LoadAll<ColorConfig>(ColorConfigPath);
            _rarityConfigs = list.ToDictionary(x => x.RarityType, x => x.Color);
        }
        private void LoadWeaponConfig()
        {
            _weaponConfigs = Resources
                .LoadAll<WeaponConfig>(WeaponConfigPath)
                .ToDictionary(x => x.WeaponTypeId, x => x);
        }
        private void LoadUnitConfig()
        {
            _unitConfigs = Resources
                .LoadAll<UnitConfig>(UnitConfigPath)
                .ToDictionary(x => x.TypeId, x => x);
        }
        private void LoadLootConfig()
        {
            _lootConfigs = Resources
                .LoadAll<LootConfig>(LootConfigPath)
                .ToDictionary(x => x.TypeId, x => x);
        }
        private void LoadLevelConfig()
        {
            _levelConfigs = Resources
                .LoadAll<LevelStaticData>("Configs/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public LootConfig LootConfigFor(in LootTypeId typeId)
        {
            if (_lootConfigs.TryGetValue(typeId, out var config))
                return config;
            
            throw new KeyNotFoundException($"No loot config found for {typeId.ToString()}");
        }

        public UnitConfig UnitConfigFor(in UnitTypeId typeId)
        {
            if (_unitConfigs.TryGetValue(typeId, out var config))
                return config;
                
            throw new KeyNotFoundException($"No unit config found for {typeId.ToString()}");
        }

        public Color32 ColorByRarity(in RarityType rarity)
        {
            if (_rarityConfigs.TryGetValue(rarity, out var color))
                return color;

            Debug.LogError($"No color found for {rarity.ToString()}");
            return Color.magenta;
        }

        public WeaponConfig WeaponConfigFor(in WeaponTypeId typeId)
        {
            if (_weaponConfigs.TryGetValue(typeId, out var config))
                return config;
            
            throw new KeyNotFoundException($"No weapon config found for {typeId.ToString()}");
        }
        
        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levelConfigs.TryGetValue(sceneKey, out var config))
                return config;
            
            throw new KeyNotFoundException($"No level config found for {sceneKey}");
        }
    }
}