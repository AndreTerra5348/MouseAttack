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
        public static Dictionary<StatsType, string> NameMap = new Dictionary<StatsType, string>()
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

        public static Dictionary<StatsType, string> FullNameMap = new Dictionary<StatsType, string>()
        {
            { StatsType.CriticalRate, "Critical Rate" },
            { StatsType.CriticalDamage, "Critical Damage" },
            { StatsType.Damage, "Damage" },
            { StatsType.Defense, "Defense" },
            { StatsType.MovementSpeed, "Mv Spd" },
            { StatsType.Health, "Health" },
            { StatsType.Mana, "Mana" },
            { StatsType.HealthRegen, "Health Regen" },
            { StatsType.ManaRegen, "Mana Regen" },
            { StatsType.CooldownReducion, "Cooldown Reduction" },
        };
    }

    public class Stats : ObservableNode
    {
        public StatsType Type { get; private set; } = StatsType.None;
        public const int MinPoints = 1;
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
            }
            get => _points;
        }

        [Export]
        public bool Editable { get; private set; } = false;

        public float Value 
        { 
            get
            {
                var value = Points * ValuePerPoint;
                return value + value * (AlteredPercentage / 100.0f);
            }
        }
        public float Percentage => Value / 100;
        float _alteredPercentage = 0;
        public float AlteredPercentage 
        {
            get => _alteredPercentage;
            set
            {
                if (_alteredPercentage == value)
                    return;
                _alteredPercentage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public override void _Ready()
        {
            if (Enum.TryParse(Name, out StatsType type))
                Type = type;
            else
                GD.PrintErr($"Stats {Name} is not part of {nameof(StatsType)}");
        }
    }
}
