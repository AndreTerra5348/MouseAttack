using Godot;
using MouseAttack.Entity;
using MouseAttack.Skill.Data;
using MouseAttack.Skill.TargetEffect.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.TargetEffect
{
    public class FloatingLabelSpawner : CommonTargetEffectSpawner
    {
        [Export]
        Color _color = Colors.Green;

        public override void SkillUsed(CommonSkillInfo info) =>
            SkillUsed(info as DamageSkillInfo);

        private void SkillUsed(DamageSkillInfo info)
        {
            if (info == null)
                return;

            FloatingLabel label = Scene.Instance<FloatingLabel>();
            label.Position = info.Target.GlobalPosition;
            label.Text = info.Damage.ToString("0.0");
            label.Color = _color;
            GridController.AddChild(label);
        }
    }
}
