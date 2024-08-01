using System;
using CodeBase.Modules.Module;
using CodeBase.Modules.WeaponModule;

namespace CodeBase.Units
{
    [Serializable]
    public class WeaponSlot
    {
        public Guid ID;
        public DimensionType SlotType;
        public WeaponModule WeaponModule = null;
    }
}