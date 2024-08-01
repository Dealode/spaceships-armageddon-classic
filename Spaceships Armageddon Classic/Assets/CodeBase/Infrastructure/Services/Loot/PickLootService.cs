using System;
using CodeBase.Enemy.Loot;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Player;
using CodeBase.Services.BattleExperience;
using CodeBase.StaticData.Loot;

namespace CodeBase.Infrastructure.Services.Loot
{
    public class PickLootService : IPickLootService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IBattleExperience _experience;

        public PickLootService(IStaticDataService staticDataService, IBattleExperience experience)
        {
            _staticDataService = staticDataService;
            _experience = experience;
        }

        public void Collect(LootBehaviour component, PlayerBehaviour playerBehaviour)
        {
            LootConfig loot = _staticDataService.LootConfigFor(component.LootType);
        
            switch (component.LootType)
            {
                case LootTypeId.ExperienceSmall:
                case LootTypeId.ExperienceMedium:
                case LootTypeId.ExperienceLarge:
                    _experience.AddExperience(loot.Value);
                    break;
                case LootTypeId.ArmorRepairSmall:
                case LootTypeId.ArmorRepairMedium:
                case LootTypeId.ArmorRepairLarge:
                    break;
                case LootTypeId.ShieldRepairSmall:
                case LootTypeId.ShieldRepairMedium:
                case LootTypeId.ShieldRepairLarge:
                    break;
                case LootTypeId.HullRepairSmall:
                case LootTypeId.HullRepairMedium:
                case LootTypeId.HullRepairLarge:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}