using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy.Loot;
using CodeBase.StaticData.Loot;
using CodeBase.Units;

namespace CodeBase.Enemy
{
    public class EnemyBehaviour : UnitBehaviour
    {
        public List<DropOneOf> DropsOneOf { get; set; }
    }
}