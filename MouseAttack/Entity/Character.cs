﻿using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;

namespace MouseAttack.Entity
{
    /// <summary>
    /// Base class for PlayerCharacter and MonsterCharacter
    /// </summary>
    public abstract class Character : ObservableNode
    {
        public event EventHandler Dead;

        float _nextLevelExperience = 100;
        public float NextLevelExperience
        {
            get => _nextLevelExperience;
            set
            {
                if (value == _nextLevelExperience)
                    return;
                _nextLevelExperience = value;
                OnPropertyChanged();
            }
        }
        float _level = 1;
        public float Level
        {
            get => _level;
            set
            {
                if (value == _level)
                    return;
                _level = value;
                OnPropertyChanged();
                OnLevelRaised();
                NextLevelExperience += NextLevelExperience*(Level / 2.0f);
                Experience = 0;
            }
        }

        float _experience = 0;
        [Export]
        public float Experience
        {
            get => _experience;
            set
            {
                if (value == _experience)
                    return;
                _experience = value;
                OnPropertyChanged();

                if (value >= NextLevelExperience)
                    Level++;
            }
        }        

        public ResourcePool Health => StatsMap[StatsType.Health] as ResourcePool;
        public Stats Damage => StatsMap[StatsType.Damage];
        public Stats Defense => StatsMap[StatsType.Defense];
        public Stats CriticalRate => StatsMap[StatsType.CriticalRate];
        public Stats CriticalDamage => StatsMap[StatsType.CriticalDamage];
        public bool IsCritical => CriticalRate.Percentage <= _random.NextDouble();
        public bool IsDead => Health.IsDepleted;
        Random _random = new Random();
        public Dictionary<StatsType, Stats> StatsMap { get; private set; } = new Dictionary<StatsType, Stats>();

        public override void _Ready()
        {
            for(int i = 0; i < GetChildCount(); i++)
            {
                Stats stats = GetChildOrNull<Stats>(i);
                if (stats == null)
                    continue;
                StatsMap[stats.Type] = stats;
            }
            Health.Depleted += OnResourceDepleted;
        }

        public bool HasStats(StatsType type) =>
            StatsMap.ContainsKey(type);

        private void OnResourceDepleted(object sender, EventArgs e)
        {
            Health.Depleted -= OnResourceDepleted;
            Dead?.Invoke(this, EventArgs.Empty);
        }

        public void Hit(float damage)
        {
            Health.Use(damage);
        }

        protected virtual void OnLevelRaised() { }
    }
}
