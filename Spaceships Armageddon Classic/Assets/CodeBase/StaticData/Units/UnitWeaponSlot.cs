using System;
using CodeBase.Modules.Module;
using CodeBase.StaticData.Weapons;

namespace CodeBase.StaticData.Units
{
    [Serializable]
    public class UnitWeaponSlot
    {
        public DimensionType dimensionType;
        public WeaponConfig WeaponConfig;
    }
}