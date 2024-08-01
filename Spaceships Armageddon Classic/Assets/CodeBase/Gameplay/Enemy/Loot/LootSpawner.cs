using System;
using CodeBase.Factory;
using CodeBase.Services.UnitRegistry;
using CodeBase.Weapon.Damage.DeathService;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy.Loot
{
    public class LootSpawner : ILootSpawner
    {
        private IDeathService _deathService;
        private IUnitRegistry _unitRegistry;
        private ILootFactory _factory;

        public LootSpawner(IDeathService deathService, IUnitRegistry unitRegistry, ILootFactory factory)
        {
            _factory = factory;
            _unitRegistry = unitRegistry;
            _deathService = deathService;
            _deathService.Killed += SpawnLootOnKilled;
        }

        private void SpawnLootOnKilled(Guid targetid, Guid attackerid)
        {
            var target = _unitRegistry.GetUnit(targetid);

            if (target is not EnemyBehaviour enemyBehaviour) return;

            for (var i = 0; i < enemyBehaviour.DropsOneOf.Count; i++)
            {
                var dropOneOf = enemyBehaviour.DropsOneOf[i];

                for (var index = 0; index < dropOneOf.Drop.Count; index++)
                {
                    var loot = dropOneOf.Drop[index];
                    if (Random.Range(0, 100) > loot.Chance)
                        continue;

                    _factory.CreateLootAt(loot.LootTypeId, target.transform.position);
                }
            }
        }
    }
}