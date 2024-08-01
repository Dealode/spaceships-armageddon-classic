using System;

namespace CodeBase.Data
{
    [Serializable]
    public class Statistic
    {
        public uint killedEnemies;
        public uint killedBosses;
        public uint passedLevels;
        
        public uint maxScore;
        public uint maxCombo;
        public uint maxKilledEnemies;
        public uint maxKilledBosses;
    }
}