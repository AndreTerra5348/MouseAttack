using Godot;
using System;

namespace MouseAttack.Character
{
    public class StatsData : Resource, ICloneable
    {
        [Export]
        public float ValuePerPoint { private set; get; } = 1.0f;
        [Export]
        public int Points { private set; get; } = 0;

        public float Value { get => Points * ValuePerPoint + _skillAlteredValue + _itemAlteredValue; }
        private float _skillAlteredValue = 0;
        private float _itemAlteredValue = 0;

        public StatsData()
        {
        }

        public StatsData(StatsData statsData)
        {
            ValuePerPoint = statsData.ValuePerPoint;
            Points = statsData.Points;
        }

        public void AddSkillAlteredValue(float value) => _skillAlteredValue += value;
        public void RemoveSkillAlteredValue(float value) => _skillAlteredValue -= value;
        public void AddItemAlteredValue(float value) => _itemAlteredValue += value;
        public void RemoveItemAlteredValue(float value) => _itemAlteredValue -= value;
        public void AddPoint(int value = 1) => Points += value;
        public void RemovePoint(int value = 1) => Points -= value;

        public virtual object Clone()
        {
            StatsData statsData = new StatsData();
            statsData.ValuePerPoint = ValuePerPoint;
            statsData.Points = Points;
            return statsData;
        }
    }
}
