using Godot;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseAttack.Characteristic
{
    public enum StatsType
    {
        None,
        Health,
        HealthRegen,
        Mana,
        ManaRegen,
        Damage,
        Defense,
        CriticalRate,
        CriticalDamage,
        MovementSpeed,
        CooldownReducion,
    }

    public class StatsNaming
    {
        public static Dictionary<StatsType, string> Map = new Dictionary<StatsType, string>()
        {
            { StatsType.CriticalRate, "Crit Rate" },
            { StatsType.CriticalDamage, "Crit Dmg" },
            { StatsType.Damage, "Dmg" },
            { StatsType.Defense, "Def" },
            { StatsType.MovementSpeed, "Mv Spd" },
            { StatsType.Health, "HP" },
            { StatsType.Mana, "MP" },
            { StatsType.HealthRegen, "HP Regen" },
            { StatsType.ManaRegen, "MP Regen" },
            { StatsType.CooldownReducion, "Cooldown Reduction" },
        };
    }

    public class Stats : ObservableNode
    {
        

        public StatsType Type { get; private set; } = StatsType.None;

        float _valuePerPoint = 1.0f;
        [Export]
        public float ValuePerPoint 
        { 
            set
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
            set
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
                var value = Points * ValuePerPoint + _alteredValue;
                return value + (value * _alteredPercentage);
            }
        }
        private float _alteredValue = 0;
        private float _alteredPercentage = 0;

        public override void _Ready()
        {
            if (Enum.TryParse<StatsType>(Name, out StatsType type))
                Type = type;
            else
                GD.PrintErr($"Stats {Name} is not part of StatsType");
        }

        public void AddAlteredValue(float value) => _alteredValue += value;
        public void RemoveAlteredValue(float value) => _alteredValue -= value;
        public void SetAlteredPercentage(float value) => _alteredPercentage = value;
        public void ResetAlteredPercentage() => _alteredPercentage = 0;
        public void AddPoint(int value = 1) => Points += value;
        public void RemovePoint(int value = 1) => Points -= value;       

    }
}
