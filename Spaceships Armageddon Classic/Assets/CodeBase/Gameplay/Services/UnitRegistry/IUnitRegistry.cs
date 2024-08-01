using System;
using System.Collections.Generic;
using CodeBase.Units;
using UniRx;

namespace CodeBase.Services.UnitRegistry
{
    public interface IUnitRegistry
    {
        public ReactiveCollection<Guid> Players { get; }
        public ReactiveCollection<Guid> Enemies { get; }
        
        void RegisterInPlayerTeam(UnitBehaviour unit);
        void RegisterInEnemyTeam(UnitBehaviour unit);
        void CleanUp();
        void Unregister(Guid unitId);
        Dictionary<Guid, UnitBehaviour> All { get; }
        List<Guid> AllyIds { get; }
        UnitBehaviour GetUnit(Guid id);
        IEnumerable<UnitBehaviour> AllActiveUnits();
        IEnumerable<Guid> AlliesOf(Guid unit);
        IEnumerable<Guid> EnemiesOf(Guid unit);
        bool IsAlive(Guid id);
    }
}