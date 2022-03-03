using Godot;
using MouseAttack.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class CastleDetectedEventArgs : EventArgs
    {
        public readonly Vector2 CollisionPoint;
        public CastleDetectedEventArgs(Vector2 collisionPoint) =>
            CollisionPoint = collisionPoint;
    }

    public class CastleDetector : Area2D
    {
        public event EventHandler<CastleDetectedEventArgs> Detected;
        public event EventHandler Lost;

        CircleShape2D Shape => ShapeOwnerGetShape(0, 0) as CircleShape2D;
        public float Range
        {
            set => Shape.Radius = value;
            get => Shape.Radius;
        }
        public bool IsInRange { get; private set; }

        public override void _EnterTree()
        {
            Connect(Signals.Area2D.AreaEntered, this, nameof(OnAreaEntered));
            Connect(Signals.Area2D.AreaExited, this, nameof(OnAreaExited));
        }

        private void OnAreaEntered(Area2D area)
        {            
            if (area is PlayerEntity)
            {
                IsInRange = true;
                var areaShape = area.ShapeOwnerGetShape(0, 0);
                var points = Shape.CollideAndGetContacts(GlobalTransform, areaShape, area.GlobalTransform);
                Detected?.Invoke(this, new CastleDetectedEventArgs((Vector2)points[0]));
            }
        }

        private void OnAreaExited(Area2D area)
        {
            if (area is PlayerEntity)
            {
                IsInRange = false;
                Lost?.Invoke(this, EventArgs.Empty);
            }
                
        }

        
    }
}
