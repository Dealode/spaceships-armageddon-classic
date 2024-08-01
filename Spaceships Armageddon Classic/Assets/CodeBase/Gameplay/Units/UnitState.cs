using UniRx;

namespace CodeBase.Units
{
    public class UnitState
    {
        public float MaxShield;
        public float MaxHull;
        
        public ReactiveProperty<float> CurrentShield = new();
        public ReactiveProperty<float> CurrentHull = new();

        public float RotationSpeed;
        public float Speed;
        public float Acceleration;
        
        public bool IsDead => CurrentHull.Value <= 0;
        
        public void AddShield(float value) => 
            CurrentShield.Value += value;

        public void AddHull(float value) => 
            CurrentHull.Value += value;
    }
}