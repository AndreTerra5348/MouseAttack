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

    public class StatsConstants
    {
        public static Dictionary<StatsType, string> NamingMap = new Dictionary<StatsType, string>()
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
            { StatsType.CooldownReducion, "Cooldown" },
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
                OnPropertyChanged(nameof(Value));
                Value++;
            }
            get => _points;
        }

        [Export]
        public bool Editable { get; private set; } = false;

        public float Value 
        { 
            get
            {
                var value = Points * ValuePerPoint + _alteredValue;
                return value + value * (_alteredPercentage / 100.0f);
            }
            set => OnPropertyChanged();
        }
        public float Percentage => Value / 100;

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

    }
}
