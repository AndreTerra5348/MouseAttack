using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System;

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
        }

        public override void _Ready()
        {
            _target = GetParent<CommonEntity>();
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
            Operate();
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            Operate();

        private void Operate()
        {
            _skill.Use(_user, _target);

            _duration--;

            if (_duration > 0)
                return;

            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;
            QueueFree();
        }
    }
}
