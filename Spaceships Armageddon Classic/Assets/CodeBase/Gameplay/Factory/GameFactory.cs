using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class GameFactory : IGameFactory
    {
        private IInstantiator _instantiator;
        
        public GameFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public SpawnPoint CreatePlayerSpawner(Vector3 at)
        {
            var prefab = Resources.Load<SpawnPoint>(AssetPath.SpawnPoint);

            var spawner = _instantiator.InstantiatePrefabForComponent<SpawnPoint>(prefab);
            spawner.transform.position = at;

            return spawner;
        }
    }
}