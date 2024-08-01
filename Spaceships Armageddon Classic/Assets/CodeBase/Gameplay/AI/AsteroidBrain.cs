using CodeBase.Services.DistanceCalculator;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.AI
{
    public class AsteroidBrain : MonoBehaviour
    {
        private UnitBehaviour _unitBehaviour;
        private UnitMove _moveSystem;
        private Transform _attackTarget;

        private IDistanceCalculator _distanceCalculator;
        
        private void Start()
        {
            _unitBehaviour = GetComponent<UnitBehaviour>();
            _moveSystem = GetComponent<UnitMove>();
        }

        public void Setup(IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
        }

        public void Update()
        {
            if (_attackTarget == null)
            {
                var target = _distanceCalculator.GetClosestPlayer(_unitBehaviour.Id);

                if (target.Item1 == null)
                    return;
                
                _attackTarget = target.Item1.transform;
            }

            if (UnitNotReached()) return;
            
            transform.LookAt(_attackTarget);
            var direction = _attackTarget.position - transform.position;
            _moveSystem.Move(direction);
        }

        private bool UnitNotReached()
        {
            return Vector3.Distance(_attackTarget.position, transform.position) <= 0;
        }
    }
}