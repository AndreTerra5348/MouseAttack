using Godot;
using System;

namespace MouseAttack.Character
{
    public class ResourceData : Resource
    {
        public EventHandler Depleted;

        [Export]
        public int MaxValue;
        public int CurrentValue { private set; get; }

        public void Reset() => CurrentValue = MaxValue;

        public void Decrease(int value = 1)
        {
            CurrentValue -= value;
            if (CurrentValue > 0)
                return;

            CurrentValue = 0;
            Depleted?.Invoke(this, new EventArgs());
        }
        public void Increase(int value = 1) => CurrentValue += value;
        public int AddMaxValue(int value) => MaxValue += value;
        public int RemoveMaxValue(int value) => MaxValue -= value;

        public override string ToString()
        {
            return String.Format("Max Value: {0} Current Value: {1}", MaxValue, CurrentValue);
        }
    }
}

