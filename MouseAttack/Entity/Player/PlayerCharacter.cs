using MouseAttack.Characteristic;
using MouseAttack.Subsystem;
using System;

namespace MouseAttack.Entity.Player
{
    public class PlayerCharacter : ResourcefulCharacter, IAttacker
    {
        public Stats ManaRegen => StatsMap[StatsType.ManaRegen];
        public Stats Damage => StatsMap[StatsType.Damage];
        public Stats CriticalRate => StatsMap[StatsType.CriticalRate];
        public Stats CriticalDamage => StatsMap[StatsType.CriticalDamage];
        public Stats CooldownReducion => StatsMap[StatsType.CooldownReducion];
        public bool IsCritical => CriticalRate.Value <= _random.Next(100);
        Random _random = new Random();

        protected override StatsType ResourceType => StatsType.Mana;

        public override void _Ready()
        {
            base._Ready();
            AddChild(new ResourceRegenerator(this));
        }

        public bool HasEnoughMana(float value) => ResourcePool.CurrentValue >= value;
        public void UseMana(float value) => ResourcePool.Use(value);
    }
}
