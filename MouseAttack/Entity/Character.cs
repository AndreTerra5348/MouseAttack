using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    /// <summary>
    /// Base class for PlayerCharacter and MonsterCharacter
    /// </summary>
    public abstract class Character : ObservableNode
    {
        public event EventHandler Dead;

        float _experience = 1;
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

        private void OnResourceDepleted(object sender, EventArgs e)
        {
            Health.Depleted -= OnResourceDepleted;
            Dead?.Invoke(this, EventArgs.Empty);
        }

        public void Hit(float damage)
        {
            Health.Use(damage);
        }
    }
}
