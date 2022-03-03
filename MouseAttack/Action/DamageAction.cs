using Godot;
using MouseAttack.Entity;
using System;

namespace MouseAttack.Action
{
    public class DamageAction : CollidableAction
    {
        [Export]
        public float Damage { get; private set; }

        public void ApplyDamage(IAttacker attacker, IDefender defender)
        {
            bool isCritical = attacker.IsCritical;
            float attackerDamage = attacker.Damage.Value;
            float AttackerCriticalDamage = attacker.CriticalDamage.Value;
            attackerDamage = isCritical ? attackerDamage * AttackerCriticalDamage : attackerDamage;
            float DefenderDefense = defender.Defense.Value;
            float finalDamage = attackerDamage + Damage - DefenderDefense;
            defender.Hit(finalDamage < 0 ? 0 : finalDamage);
        }
    }
}


