using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using MouseAttack.Constants;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;

namespace MouseAttack.Skill.WorldEffect
{
    public class DamageWorldEffect : CollidableWorldEffect
    {
        new DamageSkill Skill => base.Skill as DamageSkill;
        protected override void OnCollision(CommonEntity target) =>
            target.AddChild(new SkillOperator(User, Skill));
    }
}
