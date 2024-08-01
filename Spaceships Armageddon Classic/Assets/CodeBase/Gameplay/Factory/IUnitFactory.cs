using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Player;
using CodeBase.StaticData.Units;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.Factory
{
    public interface IUnitFactory
    {
        List<UnitBehaviour> Units { get; }
        void CleanUp();
        PlayerBehaviour CreatePlayerAt(in UnitTypeId unitTypeId, in Vector3 position, Transform parent = null);
        EnemyBehaviour CreateEnemyAt(in UnitTypeId unitTypeId, in Vector3 position, Transform parent = null);
    }
}