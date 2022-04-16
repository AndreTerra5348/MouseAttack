using Godot;
using System;

namespace MouseAttack.Characteristic
{
    public enum ResourceType
    {
        None = StatsType.None,
        Health = StatsType.Health,
        Mana = StatsType.Mana
    }

    public class ResourceChangedEventArgs : EventArgs
    {
        public readonly float Value;
        public readonly bool Increased;
        public ResourceChangedEventArgs(float value, bool increased)
        {
            Value = value;
            Increased = increased;
        }
    }

    public class ResourceUsedEventArgs : EventArgs
    {
        public readonly float Value;

        public ResourceUsedEventArgs(float value) =>
            Value = value;
    }

    public class ResourcePool : Stats
    {
        public event EventHandler Depleted;
        public event EventHandler<ResourceUsedEventArgs> Used;
        public event EventHandler<ResourceChangedEventArgs> Changed;

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

        public override void _Ready()
        {
            base._Ready();
            Reset();
        }

        public void Reset() => CurrentValue = MaxValue;

        public void Use(float value = 1.0f)
        {
            CurrentValue -= value;
            Used?.Invoke(this, new ResourceUsedEventArgs(value));
            Changed?.Invoke(this, new ResourceChangedEventArgs(value, false));
            if (CurrentValue > 0)
                return;

            CurrentValue = 0;
            Depleted?.Invoke(this, EventArgs.Empty);
        }
        public void Regenerate(float value = 1.0f)
        {
            CurrentValue += value;
            Changed?.Invoke(this, new ResourceChangedEventArgs(value, true));
            if (CurrentValue >= Value)
                CurrentValue = Value;
        }

        public override string ToString()
        {
            return String.Format("Max Value: {0} Current Value: {1}", MaxValue, CurrentValue);
        }
    }
}

