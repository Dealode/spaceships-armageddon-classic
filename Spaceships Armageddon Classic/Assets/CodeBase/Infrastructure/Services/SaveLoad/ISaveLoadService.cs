using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress(PlayerProgress progress);
        PlayerProgress LoadProgress();
    }

    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly List<IProgressWriterHolder> _progressHolders;
        private readonly IPersistentProgressService _progressService;

        public SaveLoadService(List<IProgressWriterHolder> progressHolders, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _progressHolders = progressHolders;
        }

        public void SaveProgress(PlayerProgress progress)
        {
            foreach (var progressHolder in _progressHolders)
            foreach (var progressReader in progressHolder.ProgressWriters)
                progressReader.Save(_progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}