using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using System;
using MouseAttack.Misc.UI;
using System.Collections.Generic;
using MouseAttack.Skill.TargetEffect;
using MouseAttack.Skill.WorldEffect;

namespace MouseAttack.Skill
{
    public class DamageOperator : SkillOperator
    {
        readonly PackedScene _normalFloatingLabelScene;
        readonly PackedScene _criticalFloatingLabelScene;
        readonly List<TargetEffectSpawner> _targetEffectSpawners;

        public DamageOperator() { }

        public DamageOperator(CommonEntity user, CommonSkill skill, DamageWorldEffect damageWorldEffect) 
            : base(user, skill) 
        {
            _normalFloatingLabelScene = damageWorldEffect.NormalFloatingLabelScene;
            _criticalFloatingLabelScene = damageWorldEffect.CriticalFloatingLabelScene;
            _targetEffectSpawners = damageWorldEffect.TargetEffectSpawners;
        }

        protected override void OnSkillApplied(object sender, EventArgs e) =>
            OnSkillApplied(e as DamageAppliedEventArgs);

        protected void OnSkillApplied(DamageAppliedEventArgs e)
        {
            if (e == null)
                return;

            PackedScene floatingLabelScene = e.IsCritical ? _criticalFloatingLabelScene : _normalFloatingLabelScene;
            FloatingLabel floatingLabel = floatingLabelScene.Instance<FloatingLabel>();
            floatingLabel.Text = e.Damage.ToString("0.0");
            floatingLabel.Position = Target.Position;
            Target.QueueFloatingLabel(floatingLabel);

            _targetEffectSpawners.ForEach(spawner => spawner.Spawn(Target));
        }
    }
}
