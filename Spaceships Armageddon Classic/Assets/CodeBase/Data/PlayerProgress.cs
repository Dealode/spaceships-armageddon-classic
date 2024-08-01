using System;
using CodeBase.Research;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Currency Currency = new();
        public Statistic Statistic = new();
        public Tutorial Tutorial = new();
    }
}