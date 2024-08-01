using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface IHudFactory : IProgressWriterHolder, IProgressReaderHolder
    {
        Hud Create(Vector3 at, Transform parent = null);
    }
}