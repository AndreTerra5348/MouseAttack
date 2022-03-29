using Godot;
using MouseAttack.Entity;
using MouseAttack.Misc.UI;
using MouseAttack.Skill.TargetEffect;
using System;
using System.Collections.Generic;

namespace MouseAttack.Skill.Data
{
    public class DamageAppliedEventArgs : EventArgs
    {
        public readonly float Damage;
        public readonly bool IsCritical;

        public DamageAppliedEventArgs(float damage, bool isCritical)
        {
            Damage = damage;
            IsCritical = isCritical;
        }
    }

    public class DamageSkill : CollidableSkill
    {
        [Export]
        public int Damage { get; private set; } = 1;
        [Export]
        public int Hits { get; private set; } = 1;
        public override string Tooltip =>
            $"Damage: {Damage}\n{base.Tooltip}";


        private float CalculateDamage(Character attacker, Character defender, out bool isCritical)
        {
            isCritical = attacker.IsCritical;
            float attackerDamage = attacker.Damage.Value;
            float attackerCriticalDamage = attacker.CriticalDamage.Value;
            if (isCritical)
                attackerDamage += attackerCriticalDamage;
            float defenderDefense = defender.Defense.Value;
            float finalDamage = attackerDamage + Damage - defenderDefense;
            return finalDamage < 0 ? 0 : finalDamage;
        }

        public override void Apply(CommonEntity user, CommonEntity target)
        {
            float damage = CalculateDamage(user.Character, target.Character, out bool isCritical);
            OnApplied(new DamageAppliedEventArgs(damage, isCritical));
            target.Character.Hit(damage);

            if (target.Character.IsDead)
                user.Character.Experience += target.Character.Experience;            
        }
    }
}


