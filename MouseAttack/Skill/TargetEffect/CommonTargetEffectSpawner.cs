using Godot;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using MouseAttack.World;
using System;

namespace MouseAttack.Skill.TargetEffect
{
    public abstract class CommonTargetEffectSpawner : Resource
    {
        [Export]
        protected PackedScene Scene { get; private set; }
        protected GridController GridController => TreeSharer.GetNode<GridController>();
        public abstract void SkillUsed(CommonSkillInfo info);

    }
}
