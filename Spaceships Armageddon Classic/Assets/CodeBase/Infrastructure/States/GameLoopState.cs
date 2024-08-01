namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
        }
    }
}