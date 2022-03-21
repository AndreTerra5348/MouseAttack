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
        [Export]
        int _duration = 1;
        public CommonSkill Skill { get; set; }
        public CommonEntity User { get; set; }

        public override void _Ready()
        {
            base._Ready();
            ZIndex = ZOrder.WorldEffect;
            TreeSharer.GetNode<GridController>().RoundFinished += OnRoundFinished;
            ElapseTurn();
                
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            ElapseTurn();

        async private void ElapseTurn()
        {
            _duration -= 1;

            if (_duration > 0)
                return;

            TreeSharer.GetNode<GridController>().RoundFinished -= OnRoundFinished;

            await this.CreateTimer(0.3f);

            QueueFree();
        }
    }
}
