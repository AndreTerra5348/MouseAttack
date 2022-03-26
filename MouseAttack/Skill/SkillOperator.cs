using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using MouseAttack.Misc.UI;

namespace MouseAttack.Skill
{
    public class SkillOperator : Node
    {
        CommonEntity _user = null;
        CommonEntity _target = null;
        CommonSkill _skill = null;
        int _duration = 0;

        public SkillOperator() { }

        public SkillOperator(CommonEntity user, CommonSkill skill)
        {
            _user = user;
            _skill = skill;
            _duration = _skill.Duration;
            _skill.Applied += OnSkillApplied;
        }
        

        public override void _Ready()
        {
            _target = GetParent<CommonEntity>();
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
            Operate();
        }
        private void OnSkillApplied(object sender, EventArgs e)
        {
            var floatingLabelEventArgs = e as FloatingLabelEventArgs;
            if (floatingLabelEventArgs == null)
                return;
            _target.QueueFloatingLabel(floatingLabelEventArgs.FloatingLabel);
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            Operate();

        private void Operate()
        {
            _skill.Apply(_user, _target);
            
            _duration--;

            if (_duration > 0)
                return;

            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;
            _skill.Applied -= OnSkillApplied;
            QueueFree();
        }
    }
}
