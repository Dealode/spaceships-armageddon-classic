using System;
using CodeBase.StaticData.Units;

namespace CodeBase.Units
{
    public interface IUnit
    {
        UnitState State { get; }
        Guid Id { get; set; }
        UnitTypeId TypeId { get; }
    }
}