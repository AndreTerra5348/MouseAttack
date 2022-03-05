using Godot;
using MouseAttack.Entity;

namespace MouseAttack.Action.WorldEffect
{
    public enum QueueFreeTimerStarter { AutoStart, AfterCollision }
    public abstract class CommonEffect : Node2D
    {
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
    }
}
