﻿using MouseAttack.Characteristic;
using System;

namespace MouseAttack.Entity.Player
{
    public class PlayerCharacter : Character
    {
        public ResourcePool Mana => StatsMap[StatsType.Mana] as ResourcePool;
        public Stats ManaRegen => StatsMap[StatsType.ManaRegen];
        public Stats HealthRegen => StatsMap[StatsType.HealthRegen];
        public Stats CooldownReducion => StatsMap[StatsType.CooldownReducion];

        public override void _Ready()
        {
            base._Ready();
        }

        public bool HasEnoughMana(float value) => Mana.CurrentValue >= value;
        public void UseMana(float value) => Mana.Use(value);
    }
}