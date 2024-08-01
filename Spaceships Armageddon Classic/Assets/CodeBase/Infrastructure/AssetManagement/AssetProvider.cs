using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IInstantiator _instantiator;

        public AssetProvider(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T Instantiate<T>(string path, Vector3 at, Transform parent = null) where T : Component
        {
            var prefab = Resources.Load<T>(path);
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, at, Quaternion.identity, parent);
        }
    }
}