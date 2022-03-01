using Godot;
using System;

namespace MouseAttack.Character
{
    public class ResourcePool : Stats
    {
        public event EventHandler Depleted;

        float _currentValue = 0.0f;
        public float CurrentValue 
        {
            private set
            {
                if(_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged();
                }
            }
            get => _currentValue;
        }
        public float MaxValue { get => Value; }
        public bool IsDepleted { get => CurrentValue <= 0; }
        public bool IsFull { get => CurrentValue == Value; }

        public ResourcePool()
        {
            Reset();
        }

        public ResourcePool(ResourcePool resourcePool) : base(resourcePool)
        {
            Reset();
        }

        public void Reset() => CurrentValue = MaxValue;

        public void Use(float value = 1.0f)
        {
            CurrentValue -= value;
            if (CurrentValue > 0)
                return;

            CurrentValue = 0;
            Depleted?.Invoke(this, EventArgs.Empty);
        }
        public void Regenerate(float value = 1.0f)
        {
            CurrentValue += value;
            if (CurrentValue >= Value)
                CurrentValue = Value;
        }

        public override string ToString()
        {
            return String.Format("Max Value: {0} Current Value: {1}", MaxValue, CurrentValue);
        }
    }
}

