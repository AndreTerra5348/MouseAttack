using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class PlayerDetectedEventArgs : EventArgs
    {
        public readonly Vector2 CollisionPoint;
        public PlayerDetectedEventArgs(Vector2 collisionPoint) =>
            CollisionPoint = collisionPoint;
    }

    public class PlayerDetector : Area2D
    {
        public event EventHandler<PlayerDetectedEventArgs> Detected;
        public event EventHandler Lost;

        CircleShape2D Shape => ShapeOwnerGetShape(0, 0) as CircleShape2D;
        public float Range
        {
            set => Shape.Radius = value;
            get => Shape.Radius;
        }
        public bool IsInRange { get; private set; } = false;

        public override void _EnterTree()
        {
            Connect(Signals.AreaEntered, this, nameof(OnAreaEntered));
            Connect(Signals.AreaExited, this, nameof(OnAreaExited));
        }

        private void OnAreaEntered(Area2D area)
        {
            if (!(area is PlayerEntity))
                return;

            IsInRange = true;
            Shape2D areaShape = area.ShapeOwnerGetShape(0, 0);
            Godot.Collections.Array points = Shape.CollideAndGetContacts(GlobalTransform, areaShape, area.GlobalTransform);
            Detected?.Invoke(this, new PlayerDetectedEventArgs((Vector2)points[0]));            
        }

        private void OnAreaExited(Area2D area)
        {
            if (!(area is PlayerEntity))
                return;

            IsInRange = false;
            Lost?.Invoke(this, EventArgs.Empty);
        }
    }
}
