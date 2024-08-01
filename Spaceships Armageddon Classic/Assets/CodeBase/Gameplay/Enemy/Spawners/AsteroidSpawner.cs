using System.Linq;
using CodeBase.AI;
using CodeBase.Factory;
using CodeBase.Services.DistanceCalculator;
using CodeBase.Services.UnitRegistry;
using CodeBase.StaticData.Units;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy.Spawners
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private int _asteroidCapacity;
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float2 _spawnHeight;

        [Space(10)]
        [SerializeField] private float _spawnInterval;

        private float timer;

        private IUnitFactory _unitFactory;
        private IUnitRegistry _unitRegistry;
        private IDistanceCalculator _distanceCalculator;

        private int _count = 0;
        private Transform _playerTransform;

        [Inject]
        private void Construct(IUnitFactory unitFactory, IUnitRegistry unitRegistry, IDistanceCalculator distanceCalculator)
        {
            _unitRegistry = unitRegistry;
            _unitFactory = unitFactory;
            _distanceCalculator = distanceCalculator;
        }

        void Update()
        {
            timer += Time.deltaTime;
        
            if (timer < _spawnInterval)
                return;

            timer = 0;

            if (_count >= _asteroidCapacity) return;

            _playerTransform ??= _unitRegistry.GetUnit(_unitRegistry.Players.FirstOrDefault())?.transform;

            if (_playerTransform == null) return;
            
            var randomPosition = Random.insideUnitSphere;
            randomPosition *= _spawnRadius;
            randomPosition.y = Random.Range(_spawnHeight.x, _spawnHeight.y);
            
            var enemyBehaviour = _unitFactory.CreateEnemyAt(UnitTypeId.Asteroid, randomPosition + _playerTransform.position);
            
            var stupidBrain = enemyBehaviour.gameObject.AddComponent<AsteroidBrain>();
            stupidBrain.Setup(_distanceCalculator);
            
            
            _unitRegistry.RegisterInEnemyTeam(enemyBehaviour);
        }
    }
}
