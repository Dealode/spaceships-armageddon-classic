using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initialize = "Initialize";

        private readonly GameStateMachine _stateMachine;

        private ISceneLoader _sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter() =>
            _stateMachine.Enter<LoadProgressState>();

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}