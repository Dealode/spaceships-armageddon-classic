using CodeBase.Units;
using UnityEngine;

namespace CodeBase.AI
{
    public interface IArtificialIntelligence
    {
        Transform WhereToGo(IUnit unit);
        UnitBehaviour WhatToAttack(IUnit unit);
    }
}