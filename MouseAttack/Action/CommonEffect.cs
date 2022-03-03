using Godot;
using MouseAttack.Entity;

namespace MouseAttack.Action
{
    public abstract class CommonEffect : Node2D
    {
        public CommonAction Action { get; set; }
        public Character User { get; set; }
        protected Timer QueueFreeTimer { get; private set; }

        public override void _Ready()
        {
            base._Ready();
            QueueFreeTimer = GetNode<Timer>(nameof(QueueFreeTimer));
        }
    }
}
