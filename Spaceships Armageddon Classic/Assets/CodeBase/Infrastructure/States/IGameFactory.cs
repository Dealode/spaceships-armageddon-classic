
using CodeBase.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public interface IGameFactory
    {
        SpawnPoint CreatePlayerSpawner(Vector3 at);
    }
}