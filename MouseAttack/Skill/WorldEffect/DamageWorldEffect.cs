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
using System.Collections.Generic;
using MouseAttack.Skill.TargetEffect;

namespace MouseAttack.Skill.WorldEffect
{
    public class DamageWorldEffect : CollidableWorldEffect
    {
        [Export]
        public PackedScene NormalFloatingLabelScene { get; private set; }
        [Export]
        public PackedScene CriticalFloatingLabelScene { get; private set; }
        [Export]
        public List<TargetEffectSpawner> TargetEffectSpawners { get; private set; }

        new DamageSkill Skill => base.Skill as DamageSkill;
        protected override void OnCollision(CommonEntity target) =>
            target.AddChild(new DamageOperator(User, Skill, this));
    }
}
