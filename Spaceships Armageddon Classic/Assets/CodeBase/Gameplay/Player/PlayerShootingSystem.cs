using CodeBase.Enemy;
using CodeBase.Services.DistanceCalculator;
using CodeBase.Units;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerShootingSystem : MonoBehaviour, IUnitBehaviourNeed
    {
        [SerializeField] private ShootingSystem _shootingSystem;
        
        private UnitBehaviour _unit;

        private IDistanceCalculator _distanceCalculator;

        [Inject]
        private void Construct(IDistanceCalculator distanceCalculator) => 
            _distanceCalculator = distanceCalculator;

        public void Setup(UnitBehaviour unitBehaviour) =>
            _unit = unitBehaviour;

        private void Awake()
        {
            _unit ??= GetComponentInParent<UnitBehaviour>();
            _shootingSystem ??= GetComponentInParent<ShootingSystem>();
        }

        private void Update()
        {
            var target = _distanceCalculator.GetClosestEnemy(_unit.Id);
            _shootingSystem.Shoot(target.Item1 as EnemyBehaviour, target.Item2);
        }
    }
}