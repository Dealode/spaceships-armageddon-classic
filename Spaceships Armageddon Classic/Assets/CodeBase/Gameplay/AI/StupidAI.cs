using CodeBase.Factory;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Units;
using UnityEngine;

namespace CodeBase.AI
{
    public class StupidAI : IArtificialIntelligence
    {
        private readonly IStaticDataService _staticData;
        
        
        public Transform WhereToGo(IUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public UnitBehaviour WhatToAttack(IUnit unit)
        {
            throw new System.NotImplementedException();
        }
    }
}