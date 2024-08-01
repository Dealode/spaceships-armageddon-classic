using CodeBase.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Modules.WeaponModule
{
    public interface IShootStrategy
    {
        UniTask Shoot(UnitBehaviour unit, WeaponModule weapon, UnitBehaviour target);
    }
}