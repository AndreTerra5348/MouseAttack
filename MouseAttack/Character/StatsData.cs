using Godot;
using System;

namespace MouseAttack.Character
{
    public class StatsData : Resource
    {
        [Export]
        public float ValuePerPoint = 1.0f;
        [Export]
        public int Points = 0;

        public float Value { get => Points * ValuePerPoint + AbnormalValue; }
        protected int AbnormalValue = 0;

        public void AddAbnormalValue(int value) => AbnormalValue += value;
        public void RemoveAbnormalValue(int value) => AbnormalValue -= value;
    }
}
