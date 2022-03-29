using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using MouseAttack.Skill.WorldEffect;

namespace MouseAttack.Skill
{
    public class SkillOperator : Node
    {
        protected CommonEntity User { get; private set; } = null;
        protected CommonEntity Target { get; private set; } = null;
        protected CommonSkill Skill { get; private set; } = null;
        int _duration = 0;

        public SkillOperator() { }

        public SkillOperator(CommonEntity user, CommonSkill skill)
        {
            User = user;
            Skill = skill;
            _duration = skill.Duration;            
        }
        

        public override void _Ready()
        {
            Target = GetParent<CommonEntity>();
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
            Skill.Applied += OnSkillApplied;
            Operate();
        }
        protected virtual void OnSkillApplied(object sender, EventArgs e)
        {
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            Operate();

        private void Operate()
        {
            Skill.Apply(User, Target);
            
            _duration--;

            if (_duration > 0)
                return;

            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;
            Skill.Applied -= OnSkillApplied;
            QueueFree();
        }
    }
}
