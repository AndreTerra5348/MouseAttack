using Godot;

namespace MouseAttack.Action
{
    public class CollidableEffect : CommonEffect
    {
        [Export]
        public NodePath Area2DPath;

        public override void _Ready()
        {
            base._Ready();
            var collidableAction = CommonAction as CollidableAction;
            var area2d = GetNode<Area2D>(Area2DPath);
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;
            shape.Radius = collidableAction.Radius;

            area2d.Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
        }

        protected virtual void OnBodyEntered(Node body)
        {
            
        }
    }
}

