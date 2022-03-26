using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using MouseAttack.Extensions;

namespace MouseAttack.Skill.WorldEffect
{
    public abstract class CommonWorldEffect : Node2D
    {
        public CommonSkill Skill { get; set; }
        public CommonEntity User { get; set; }

        public override void _Ready() =>
            ZIndex = ZOrder.WorldEffect;
    }
}
