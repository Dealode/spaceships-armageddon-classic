using System;
using System.Collections.Generic;
using CodeBase.Services.UnitRegistry;
using CodeBase.Units;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.DistanceCalculator
{
    public class DistanceCalculator : IDistanceCalculator, ILateTickable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly Dictionary<Guid, Dictionary<Guid, float>> _distancesToPlayers = new();
        
        private readonly IUnitRegistry _unitRegistry;

        private const int Interval = 10;

        public DistanceCalculator(IUnitRegistry unitRegistry)
        {
            _unitRegistry = unitRegistry;
            
            _unitRegistry.Players
                .ObserveAdd()
                .Subscribe(x => OnPlayerUnitAdded(x.Value))
                .AddTo(_compositeDisposable);

            _unitRegistry.Players
                .ObserveRemove()
                .Subscribe(x => OnEnemyUnitRemoved(x.Value))
                .AddTo(_compositeDisposable);

            _unitRegistry.Enemies
                .ObserveAdd()
                .Subscribe(x => OnEnemyUnitAdded(x.Value))
                .AddTo(_compositeDisposable);

            _unitRegistry.Enemies
                .ObserveRemove()
                .Subscribe(x => OnEnemyUnitRemoved(x.Value))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
        
        private void OnPlayerUnitAdded(Guid guid)
        {
            _distancesToPlayers.Add(guid, new Dictionary<Guid, float>());
        }

        private void OnPlayerUnitRemoved(Guid guid)
        {
            _distancesToPlayers.Remove(guid);
        }
        
        private void OnEnemyUnitRemoved(Guid guid)
        {
            foreach (var playerGuid in _unitRegistry.Players)
            {
                _distancesToPlayers[playerGuid].Remove(guid);
            }
        }

        private void OnEnemyUnitAdded(Guid guid)
        {
            foreach (var playerGuid in _unitRegistry.Players)
            {
                _distancesToPlayers[playerGuid].Add(guid, float.MaxValue);
            }
        }

        public void LateTick()
        {
            if (Time.frameCount % Interval != 0)
                return;
            
            UpdateDistancesPlayersToEnemies();
        }

        public (UnitBehaviour, float) GetClosestPlayer(Guid myEnemyId)
        {
            var minDistance = float.MaxValue;
            Guid minimallyDistantPlayer = default;
            
            foreach (var playerGuid in _unitRegistry.Players)
            {
                if (_distancesToPlayers.TryGetValue(playerGuid, out var distancesToPlayer))
                {
                    if (distancesToPlayer.TryGetValue(myEnemyId, out var distance))
                    {
                        if (!(distance < minDistance)) continue;
                        
                        minDistance = distance;
                        minimallyDistantPlayer = playerGuid;
                    }
                    else
                    {
                        Debug.LogError($"Player {playerGuid} has no distance to unit {myEnemyId}!");
                    }
                }
                else
                {
                    Debug.LogError($"Player {playerGuid} has no distances to other units!");
                }
            }
            
            return (minimallyDistantPlayer == default
                    ? null
                    : _unitRegistry.GetUnit(minimallyDistantPlayer),
                minDistance);
        }

        public (UnitBehaviour, float) GetClosestEnemy(Guid myPlayerId)
        {
            var minDistance = float.MaxValue;
            Guid minimallyDistanceEnemy = default;
            
            if (_distancesToPlayers.TryGetValue(myPlayerId, out var playerDistances))
            {
                foreach (var distanceToEnemy in playerDistances)
                {
                    if (!(distanceToEnemy.Value < minDistance)) continue;
                    
                    minDistance = distanceToEnemy.Value;
                    minimallyDistanceEnemy = distanceToEnemy.Key;
                }
            }
            else
            {
                Debug.LogError($"Player {myPlayerId} has no distances to other units!");
            }
            return (minimallyDistanceEnemy == default
                    ? null
                    : _unitRegistry.GetUnit(minimallyDistanceEnemy),
                minDistance);
        }

        public float GetDistanceToEnemy(Guid player, Guid enemy)
        {
            return _distancesToPlayers[player][enemy];
        }

        private void UpdateDistancesPlayersToEnemies()
        {
            for (var i = 0; i < _unitRegistry.Players.Count; i++)
            {
                var playerGuid = _unitRegistry.Players[i];
                if (_distancesToPlayers.ContainsKey(playerGuid))
                {
                    UpdateDistancesThePlayerToEnemies(playerGuid);
                }
            }
        }

        private void UpdateDistancesThePlayerToEnemies(Guid playerGuid)
        {
            for (var i = 0; i < _unitRegistry.Enemies.Count; i++)
            {
                var enemyGuid = _unitRegistry.Enemies[i];
                if (_distancesToPlayers[playerGuid].ContainsKey(enemyGuid))
                {
                    UpdateDistanceThePlayerToTheEnemy(playerGuid, enemyGuid);
                }
            }
        }

        private void UpdateDistanceThePlayerToTheEnemy(Guid playerGuid, Guid enemyGuid)
        {
            var playerTransform = _unitRegistry.GetUnit(playerGuid).transform;
            var enemyTransform = _unitRegistry.GetUnit(enemyGuid).transform;
            
            _distancesToPlayers[playerGuid][enemyGuid] = Vector3.Distance(enemyTransform.position, playerTransform.position);
        }
    }
}