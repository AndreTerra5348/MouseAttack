using Godot;
using MouseAttack.Entity;
using MouseAttack.Skill.TargetEffect;
using System;

namespace MouseAttack.Skill.Data
{
    public class DamageSkillInfo : CommonSkillInfo
    {
        public readonly float Damage;
        public DamageSkillInfo(CommonEntity target, float damage)
            : base(target) => Damage = damage;

    }
    public class DamageSkill : CollidableSkill
    {
        [Export]
        public int Damage { get; private set; } = 1;
        [Export]
        public int Hits { get; private set; } = 1;

        public override string Tooltip =>
            $"Damage: {Damage}\n" + base.Tooltip;

        private float CalculateDamage(Character attacker, Character defender)
        {
            bool isCritical = attacker.IsCritical;
            float attackerDamage = attacker.Damage.Value;
            float attackerCriticalDamage = attacker.CriticalDamage.Value;
            if (isCritical)
                attackerDamage += attackerCriticalDamage;
            float defenderDefense = defender.Defense.Value;
            float finalDamage = attackerDamage + Damage - defenderDefense;
            return finalDamage < 0 ? 0 : finalDamage;
        }

        public override void Use(CommonEntity user, CommonEntity target)
        {
            float damage = CalculateDamage(user.Character, target.Character);
            target.Character.Hit(damage);

            if (target.Character.IsDead)
                user.Character.Experience += target.Character.Experience;

            TargetEffectSpaners.ForEach(tf => tf.SkillUsed(new DamageSkillInfo(target, damage)));

        }
    }
}


