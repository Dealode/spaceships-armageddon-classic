using System;
using CodeBase.Units;

namespace CodeBase.Services.DistanceCalculator
{
    public interface IDistanceCalculator
    {
        (UnitBehaviour, float) GetClosestPlayer(Guid myEnemyId);
        (UnitBehaviour, float) GetClosestEnemy(Guid myPlayerId);
        float GetDistanceToEnemy(Guid player, Guid enemy);
    }
}