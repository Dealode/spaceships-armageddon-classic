using System;
using System.Collections.Generic;
using CodeBase.Modules.Module;
using UnityEngine.Serialization;

namespace CodeBase.Research
{
    [Serializable]
    public class Research
    {
        [FormerlySerializedAs("ModuleTypeId")] public WeaponTypeId weaponTypeId;
        public bool IsInvestigated;
        public List<Research> Dependents;
        public Research(WeaponTypeId weaponTypeId, bool isResearched)
        {
            this.weaponTypeId = weaponTypeId;
            IsInvestigated = isResearched;
            Dependents = new();
        }
    }
}