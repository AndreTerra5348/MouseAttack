using Godot;
using MouseAttack.Entity;
using System;

namespace MouseAttack.Action.WorldEffect
{
    public class ActionEventArgs : EventArgs
    {
        public readonly CommonEntity Target;
        public readonly float Value;
        public ActionEventArgs(CommonEntity target, float value)
        {
            Value = value;
            Target = target;
        }
    }
    public enum QueueFreeTimerStarter { AutoStart, AfterCollision }
    public abstract class CommonWorldEffect : Node2D
    {
        public event EventHandler<ActionEventArgs> ActionApplied;

        [Export]
        protected QueueFreeTimerStarter QueueFreeTimerStarter { get; private set; } = QueueFreeTimerStarter.AutoStart;
        public CommonAction Action { get; set; }
        public Character User { get; set; }
        protected Timer QueueFreeTimer { get; private set; }
        public override void _Ready()
        {
            base._Ready();
            QueueFreeTimer = GetNode<Timer>(nameof(QueueFreeTimer));
            QueueFreeTimer.Autostart = QueueFreeTimerStarter == QueueFreeTimerStarter.AutoStart;
        }

        protected void OnActionApplied(CommonEntity target, float value) => 
            ActionApplied?.Invoke(this, new ActionEventArgs(target, value));
    }
}
