using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.UnitRegistry;
using CodeBase.StaticData.Units;
using CodeBase.UI;
using CodeBase.Units;
using UnityEngine;
using Zenject;

namespace CodeBase.Factory
{
    public class HudFactory : IHudFactory
    {
        private readonly IUnitRegistry _unitRegistry;
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _instantiator;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new();

        public HudFactory(IAssetProvider asset, IUnitRegistry unitRegistry)
        {
            _unitRegistry = unitRegistry;
            _assets = asset;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public Hud Create(Vector3 at, Transform parent = null)
        {
            var go = _assets.Instantiate<Hud>(AssetPath.HudPath, at, parent);

            var currentPlayer = FindLocalPlayer(_unitRegistry.Players);
            
            go.GetComponentInChildren<PlayerUI>().Construct(currentPlayer.State);  

            RegisterProgressWriters(go);

            return go;
        }

        private UnitBehaviour FindLocalPlayer(IReadOnlyList<Guid> unitRegistryPlayers)
        {
            for (var index = 0; index < unitRegistryPlayers.Count; index++)
            {
                var unit = unitRegistryPlayers[index];
                var unitBehaviour = _unitRegistry.GetUnit(unit);
                if (unitBehaviour.IsLocal)
                    return unitBehaviour;
            }

            return null;
        }

        private void RegisterProgressWriters(Hud go)
        {
            foreach (ISavedProgressWriter progressWriter in go.GetComponentsInChildren<ISavedProgressWriter>())
                RegisterProgressWriter(progressWriter);
        }

        private void RegisterProgressWriter(ISavedProgressWriter progressWriter)
        {
            if (progressWriter is ISavedProgressReader progressReader)
                ProgressReaders.Add(progressReader);
            
            ProgressWriters.Add(progressWriter);
        }
    }
}