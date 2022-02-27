using Godot;
using System;

namespace MouseAttack.Character
{
    public class ResourceData : StatsData
    {
        public EventHandler Depleted;
        public float CurrentValue { private set; get; }
        public float MaxValue { get => Value; }
        public bool IsDepleted { get => CurrentValue <= 0; }

        public ResourceData()
        {
        }

        public ResourceData(ResourceData resourceData) : base(resourceData)
        {
            Reset();
        }

        public void Reset() => CurrentValue = MaxValue;

        public void Decrease(float value = 1.0f)
        {
            CurrentValue -= value;
            if (CurrentValue > 0)
                return;

            CurrentValue = 0;
            Depleted?.Invoke(this, new EventArgs());
        }
        public void Increase(float value = 1.0f) => CurrentValue += value;

        public override string ToString()
        {
            return String.Format("Max Value: {0} Current Value: {1}", MaxValue, CurrentValue);
        }
    }
}

