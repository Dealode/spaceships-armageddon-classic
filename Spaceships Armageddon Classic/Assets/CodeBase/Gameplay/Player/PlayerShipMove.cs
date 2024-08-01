using CodeBase.Infrastructure.Services.Input;
using CodeBase.Units;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerShipMove : MonoBehaviour
    {
        public UnitMove unitMove;
        
        private IInputService _inputService;

        [Inject]
        private void Construct(IInputService inputService) => 
            _inputService = inputService;

        private void Awake() => 
            unitMove ??= GetComponentInParent<UnitMove>();

        private void Update()
        {
            if (_inputService.Axis3D.magnitude < 0.01f)
                return;
            
            unitMove.Move(_inputService.Axis3D);
        }
    }
}