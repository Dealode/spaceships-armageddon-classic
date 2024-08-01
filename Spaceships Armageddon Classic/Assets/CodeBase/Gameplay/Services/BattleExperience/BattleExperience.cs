using UniRx;
using UnityEngine;

namespace CodeBase.Services.BattleExperience
{
    public class BattleExperience : IBattleExperience
    {
        private readonly ReactiveProperty<float> _teamCollectedExperience = new(0);
        
        public void AddExperience(int experience)
        {
            _teamCollectedExperience.Value += experience;
            Debug.Log($"BattleExperience: {_teamCollectedExperience}");
        }
    }
}