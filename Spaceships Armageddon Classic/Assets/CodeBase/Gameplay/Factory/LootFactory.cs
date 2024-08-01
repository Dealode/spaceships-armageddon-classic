using CodeBase.Enemy.Loot;
using CodeBase.Infrastructure.StaticData;
using CodeBase.StaticData.Loot;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    class LootFactory : ILootFactory
    {
        private IStaticDataService _staticDataService;
        private IInstantiator _instantiator;

        public LootFactory(IStaticDataService staticDataService, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void CreateLootAt(LootTypeId lootKey, Vector3 transformPosition)
        {
            var config = _staticDataService.LootConfigFor(lootKey);
            
            _instantiator
                .InstantiatePrefabForComponent<LootBehaviour>(config.lootBehaviours, transformPosition, Quaternion.identity, null)
                .Initialize(lootKey);
        }
    }
}