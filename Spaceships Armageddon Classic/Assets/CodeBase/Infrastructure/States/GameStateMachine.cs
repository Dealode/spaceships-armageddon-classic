using System;
using System.Collections.Generic;
using CodeBase.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services.UnitRegistry;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        public IExitableState ActiveState { get; private set; }

        public GameStateMachine(
            ISceneLoader sceneLoader, IStaticDataService staticDataService,
            IUnitFactory unitFactory, IHudFactory hudFactory, IGameFactory gameFactory,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            List<IProgressWriterHolder> writerHolders, List<IProgressReaderHolder> readerHolders,
            IUnitRegistry unitRegistry, Curtain curtain)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadProgressState)] = new LoadProgressState(this, progressService, saveLoadService, staticDataService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, staticDataService, unitFactory, hudFactory, gameFactory, curtain, writerHolders, readerHolders, progressService, unitRegistry),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedIState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            ActiveState?.Exit();
            var state = GetState<TState>();
            ActiveState = state;

            return state;
        }
        
        private TState GetState<TState>() where TState : class, IExitableState =>
                _states[typeof(TState)] as TState;
    }
}