using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using MouseAttack.Extensions;

namespace MouseAttack.Skill.WorldEffect
{
    public enum QueueFreeMode
    {
        Collision,
        Timeout
    }
    public abstract class CommonWorldEffect : Node2D
    {
        public event EventHandler QueueFreed;
        public CommonSkill Skill { get; set; }
        public CommonEntity User { get; set; }

        [Export]
        protected float QueueFreeDelay { get; private set; } = 0.3f;
        [Export]
        protected QueueFreeMode QueueFreeMode { get; private set; } = QueueFreeMode.Timeout;

        GridController GridController => TreeSharer.GetNode<GridController>();

        int _elapsedDuration = 0;
        public override void _Ready()
        {
            ZIndex = ZOrder.WorldEffect;
            GridController.RoundFinished += OnRoundFinished;
            _elapsedDuration = Skill.EffectDuration;
            ElapseTurn();
        }

        private void OnRoundFinished(object sender, EventArgs e) =>
            ElapseTurn();

        async private void ElapseTurn()
        {
            _elapsedDuration--;

            if (_elapsedDuration > 0)
                return;

            GridController.RoundFinished -= OnRoundFinished;

            if (QueueFreeMode != QueueFreeMode.Timeout)
                return;

            await this.CreateTimer(QueueFreeDelay);
            QueueFree();
        }

        public new void QueueFree()
        {
            QueueFreed?.Invoke(this, EventArgs.Empty);
            base.QueueFree();
        }
    }
}
