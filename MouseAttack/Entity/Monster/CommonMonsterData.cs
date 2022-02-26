using Godot;
using MouseAttack.Character;
using System;

namespace MouseAttack.Entity.Monster
{
    public class CommonMonsterData : Resource
    {
        [Export]
        public ResourceData Health;
        [Export]
        public StatsData Damage;
        [Export]
        public StatsData MovementSpeed;

        public void ResetResources() => Health.Reset();
    }
}

