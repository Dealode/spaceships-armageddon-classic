using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        
        public List<PlayerSpawnerData> PlayerSpawners; 
    }
}