using CodeBase.Enemy.Loot;
using CodeBase.Infrastructure.Services.Loot;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerLootPicker : MonoBehaviour
    {
        private IPickLootService _pickLootService;
        private PlayerBehaviour _playerBehaviour;

        [Inject]
        private void Construct(IPickLootService pickLootService) => 
            _pickLootService = pickLootService;

        private void Start() => 
            _playerBehaviour = GetComponentInParent<PlayerBehaviour>();

        private void OnTriggerEnter(Collider coll) => Pick(coll);

        private void Pick(Collider coll)
        {
            if (!coll.TryGetComponent(out LootBehaviour loot)) return;
        
            _pickLootService.Collect(loot, _playerBehaviour);
            loot.gameObject.SetActive(false);
        }
    }
}