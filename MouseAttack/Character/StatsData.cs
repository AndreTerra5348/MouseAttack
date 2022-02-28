using Godot;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseAttack.Character
{
    public class StatsData : Resource, INotifyPropertyChanged
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

        public float Value { get => Points * ValuePerPoint + _skillAlteredValue + _itemAlteredValue; }
        private float _skillAlteredValue = 0;
        private float _itemAlteredValue = 0;
        

        public StatsData()
        {
        }

        public StatsData(StatsData statsData)
        {
            Console.WriteLine("stats data");
            ValuePerPoint = statsData.ValuePerPoint;
            Points = statsData.Points;
            Console.WriteLine(ValuePerPoint);
            Console.WriteLine(Points);
        }

        public void AddSkillAlteredValue(float value) => _skillAlteredValue += value;
        public void RemoveSkillAlteredValue(float value) => _skillAlteredValue -= value;
        public void AddItemAlteredValue(float value) => _itemAlteredValue += value;
        public void RemoveItemAlteredValue(float value) => _itemAlteredValue -= value;
        public void AddPoint(int value = 1) => Points += value;
        public void RemovePoint(int value = 1) => Points -= value;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
