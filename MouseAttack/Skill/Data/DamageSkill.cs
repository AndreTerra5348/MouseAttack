using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc.UI;
using MouseAttack.Skill.TargetEffect;
using System;
using System.Collections.Generic;

namespace MouseAttack.Skill.Data
{
    public class DamageSkill : CollidableSkill
    {
        public int Damage { get; private set; }

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Damage: {Damage}", Colors.Red));
            return tooltipInfo;
        }

        private float CalculateDamage(Character attacker, Character defender, out bool isCritical)
        {
            isCritical = attacker.IsCritical;
            float attackerDamage = attacker.Damage.Value;
            float attackerCriticalDamage = attacker.CriticalDamage.Percentage;
            attackerDamage *= (isCritical ? attackerCriticalDamage : 1);
            float defenderDefense = defender.Defense.Value;
            float finalDamage = attackerDamage + Damage - defenderDefense;
            return finalDamage < 0 ? 0 : finalDamage;
        }

        /// <summary>
        /// Called by the WorldEffect when there's a collision with the target
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        public override void Apply(CommonEntity user, CommonEntity target)
        {
            ApplyAction = () =>
            {
                float damage = CalculateDamage(user.Character, target.Character, out bool isCritical);
                SpawnFloatingLabel(target, damage.ToString("0.0"), isCritical ? 15 : 13);
                SpawnTargetEffects(target);

                target.Character.Hit(damage);

                if (target.Character.IsDead)
                    user.Character.Experience += target.Character.Experience;

            };

            ApplyAction();
        }
    }
}


