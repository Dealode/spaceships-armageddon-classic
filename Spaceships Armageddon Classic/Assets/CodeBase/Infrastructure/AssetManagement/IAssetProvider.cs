using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        T Instantiate<T>(string path, Vector3 at, Transform parent = null) where T : Component;
    }
}