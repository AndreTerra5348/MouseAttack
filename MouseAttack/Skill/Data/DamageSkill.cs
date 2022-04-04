using Godot;
using MouseAttack.Entity;
using MouseAttack.Item.Tooltip;
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
        public int Damage { get; private set; }
        public int Hits { get; private set; }

        public override Color Color => Colors.Red;

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipinfo = base.GetTooltipInfo();
            tooltipinfo.Push(new TooltipInfo($"Hits: {Hits}", Colors.MediumVioletRed));
            tooltipinfo.Push(new TooltipInfo($"Damage: {Damage}", Colors.Red));
            return tooltipinfo;
        }

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

        public override void ItemDropped(int monsterLevel)
        {

        }
    }
}


