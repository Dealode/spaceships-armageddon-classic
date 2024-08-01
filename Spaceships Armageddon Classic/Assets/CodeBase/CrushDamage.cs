using CodeBase.Modules.WeaponModule;
using CodeBase.StaticData.Units;
using CodeBase.Units;
using CodeBase.Weapon.Damage.Appliers;
using UnityEngine;
using Zenject;

public class CrushDamage : MonoBehaviour
{
    [SerializeField] UnitWeaponSlot _weaponConfig;
    
    private IDamageApplier _damageApplier;
    private UnitBehaviour _I;
    private UnitBehaviour _unit;
    private WeaponModule _weaponModule;
    
    [Inject]
    private void Construct(IDamageApplier damageApplier)
    {
        _damageApplier = damageApplier;
    }

    private void Start()
    {
        _I = GetComponent<UnitBehaviour>();
        _weaponModule = WeaponModule.FromUnitWeaponSlot(_weaponConfig);
    }

    private void Update()
    {
        _weaponModule.TickCooldown(Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent<UnitBehaviour>(out var unit)) return;
        
        _unit = unit;

        if (!_weaponModule.IsReady) return;
        
        _damageApplier.Apply(_weaponModule, _weaponModule.damage, _unit, _I);
        _weaponModule.StartCooldown();
    }

    private void OnCollisionStay(Collision other)
    {
        if (!_weaponModule.IsReady) return;
        
        _damageApplier.Apply(_weaponModule, _weaponModule.damage, _unit, _I);
        _weaponModule.StartCooldown();
    }

    private void OnCollisionExit(Collision other)
    {
        _unit = null;
    }
}
