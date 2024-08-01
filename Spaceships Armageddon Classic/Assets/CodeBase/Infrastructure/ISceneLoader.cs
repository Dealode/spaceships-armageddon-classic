using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader
    { 
        UniTask Load(string sceneName, Action onLoaded = null);
        void LoadAdditive(string hudScene, object o);
    }

    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                return;
            }
            await SceneManager.LoadSceneAsync(sceneName);
            onLoaded?.Invoke();
        }

        public async void LoadAdditive(string hudScene, object o)
        {
            await SceneManager.LoadSceneAsync(hudScene, LoadSceneMode.Additive);
        }
    }
}