using Godot;
using MouseAttack.Misc;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseAttack.Character
{
    public class Stats : ObservableResource
    {

        float _valuePerPoint = 1.0f;
        [Export]
        public float ValuePerPoint 
        { 
            private set
            {
                if (_valuePerPoint == value)
                    return;
                _valuePerPoint = value;
                OnPropertyChanged();
            }
            get => _valuePerPoint;
        }

        int _points = 1;
        [Export]
        public int Points
        { 
            private set
            {
                if (_points == value)
                    return;
                _points = value;
                OnPropertyChanged();
            }
            get => _points;
        }

        public float Value 
        { 
            get
            {
                var value = Points * ValuePerPoint + _alteredValue + _skillAlteredValue;
                return value + (value * _alteredPercentage);
            }
        }
        private float _alteredValue = 0;
        private float _alteredPercentage = 0;
        private float _skillAlteredValue = 0;

        public Stats()
        {
        }

        public Stats(Stats stats)
        {
            ValuePerPoint = stats.ValuePerPoint;
            Points = stats.Points;
        }

        public void AddAlteredValue(float value) => _alteredValue += value;
        public void RemoveAlteredValue(float value) => _alteredValue -= value;
        public void SetAlteredPercentage(float value) => _alteredPercentage = value;
        public void ResetAlteredPercentage() => _alteredPercentage = 0;
        public void AddSkillAlteredValue(float value) => _skillAlteredValue += value;
        public void RemoveSkillAlteredValue(float value) => _skillAlteredValue -= value;
        public void AddPoint(int value = 1) => Points += value;
        public void RemovePoint(int value = 1) => Points -= value;       

    }
}
