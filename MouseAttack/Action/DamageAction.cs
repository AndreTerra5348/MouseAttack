using Godot;
using MouseAttack.Entity;
using System;

namespace MouseAttack.Action
{
    public class DamageAction : CollidableAction
    {
        [Export]
        public float Damage { get; private set; } = 1;
        [Export]
        public int Hits { get; private set; } = 1;
        [Export]
        public float DamageTimeout { get; private set; } = 0;

        public void ApplyDamage(Character attacker, Character defender)
        {
            bool isCritical = attacker.IsCritical;
            float attackerDamage = attacker.Damage.Value;
            float attackerCriticalDamage = attacker.CriticalDamage.Value;
            attackerDamage = isCritical ? attackerDamage * attackerCriticalDamage : attackerDamage;
            float defenderDefense = defender.Defense.Value;
            float finalDamage = attackerDamage + Damage - defenderDefense;
            defender.Hit(finalDamage < 0 ? 0 : finalDamage);
        }
    }
}


