using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService
    {
        public Vector2 Axis { get; }
        Vector3 Axis3D { get; }
    }

    public class InputService : IInputService
    {
        public Vector2 Axis => _playerActions.Player.Move.ReadValue<Vector2>();
        public Vector3 Axis3D => new(Axis.x, 0, Axis.y);
        
        private PlayerControls _playerActions;

        public InputService()
        {
            _playerActions = new PlayerControls();
            _playerActions.Enable();
        }
    }
}