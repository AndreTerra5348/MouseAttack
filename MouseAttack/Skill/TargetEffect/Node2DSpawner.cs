using Godot;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.TargetEffect
{
    public class Node2DSpawner : CommonTargetEffectSpawner
    {
        public override void SkillUsed(CommonSkillInfo info)
        {
            Node2D instance = Scene.Instance<Node2D>();
            GridController.AddChild(instance);
            instance.GlobalPosition = info.Target.GlobalPosition;
        }
            
    }
}
