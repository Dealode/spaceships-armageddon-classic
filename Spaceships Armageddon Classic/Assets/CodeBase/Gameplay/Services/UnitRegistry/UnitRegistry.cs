using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Units;
using UniRx;

namespace CodeBase.Services.UnitRegistry
{
    public class UnitRegistry : IUnitRegistry
    {
        public ReactiveCollection<Guid> Players { get; } = new();
        public ReactiveCollection<Guid> Enemies { get; } = new();
        public List<Guid> AllyIds { get; private set; } = new();
        
        public Dictionary<Guid, UnitBehaviour> All { get; } = new();
        
        public void RegisterInPlayerTeam(UnitBehaviour unit)
        {
            if (!Players.Contains(unit.Id)) 
                Players.Add(unit.Id);
            
            All[unit.Id] = unit;
            
            UpdateCashes();
        }

        public void RegisterInEnemyTeam(UnitBehaviour unit)
        {
            if (!Enemies.Contains(unit.Id)) 
                Enemies.Add(unit.Id);
            
            All[unit.Id] = unit;
            
            UpdateCashes();
        }

        public void CleanUp()
        {
            Players.Clear();
            Enemies.Clear();
            All.Clear();
            AllyIds.Clear();
        }

        public void Unregister(Guid unitId)
        {
            if (Players.Contains(unitId))
            {
                Players.Remove(unitId);
            }

            if (Enemies.Contains(unitId))
            {
                Enemies.Remove(unitId);
            }

            if (All.ContainsKey(unitId))
                All.Remove(unitId);
            
            UpdateCashes();
        }

        public bool IsAlive(Guid id) => All.ContainsKey(id);
        
        public UnitBehaviour GetUnit(Guid id)
        {
            return All.TryGetValue(id, out var unit)
                ? unit
                : throw new ArgumentException($"Unit with id {id} not found");
        }

        public IEnumerable<UnitBehaviour> AllActiveUnits() => All.Values;

        public IEnumerable<Guid> AlliesOf(Guid unit) => 
            Players.Contains(unit) ? Players : Enemies;

        public IEnumerable<Guid> EnemiesOf(Guid unit) => 
            Players.Contains(unit) ? Enemies : Players;

        private void UpdateCashes()
        {
            AllyIds = All.Keys.ToList();
        }
    }
}