using CodeBase.Enemy.Loot;
using CodeBase.Player;

namespace CodeBase.Infrastructure.Services.Loot
{
    public interface IPickLootService
    {
        void Collect(LootBehaviour component, PlayerBehaviour playerBehaviour);
    }
}