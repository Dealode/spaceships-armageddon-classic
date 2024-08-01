using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using CodeBase.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.UnitRegistry;
using CodeBase.StaticData.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedIState<string>
    {
        private GameStateMachine _stateMachine;
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;
        private IUnitFactory _unitFactory;
        private Curtain _curtain;
        private IHudFactory _hudFactory;
        
        private readonly List<IProgressWriterHolder> _writerHolders;
        private readonly List<IProgressReaderHolder> _readerHolders;
        private readonly IPersistentProgressService _progressService;
        private readonly IUnitRegistry _unitRegistry;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, IUnitFactory unitFactory,
            IHudFactory hudFactory, IGameFactory gameFactory,
            Curtain curtain,
            List<IProgressWriterHolder> writerHolders, List<IProgressReaderHolder> readerHolders,
            IPersistentProgressService progressService, IUnitRegistry unitRegistry)
        {
            _gameFactory = gameFactory;
            _unitRegistry = unitRegistry;
            _progressService = progressService;
            _readerHolders = readerHolders;
            _writerHolders = writerHolders;
            _hudFactory = hudFactory;
            _curtain = curtain;
            _unitFactory = unitFactory;
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            foreach (var progressHolder in _writerHolders) progressHolder.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Update()
        {
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReaderHolder progressHolder in _readerHolders)
            foreach (ISavedProgressReader progressReader in progressHolder.ProgressReaders)
                progressReader.Read(_progressService);
        }

        private void InitGameWorld()
        {
            var sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);

            var spawnPoint = _gameFactory.CreatePlayerSpawner(levelData.PlayerSpawners.First().Position);

            SpawnPlayer(spawnPoint);

            _hudFactory.Create(Vector3.zero);
        }

        private void SpawnPlayer(SpawnPoint spawnPoint)
        {
            var unit = _unitFactory.CreatePlayerAt(UnitTypeId.HeavyFighter, spawnPoint.transform.position,
                spawnPoint.transform.root);
            _unitRegistry.RegisterInPlayerTeam(unit);

            if (Camera.main != null)
                Camera.main.GetComponent<CinemachineVirtualCamera>().Follow = unit.transform;
        }
    }
}