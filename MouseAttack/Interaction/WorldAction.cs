using Godot;

namespace MouseAttack.Interaction
{
    public class WorldAction : CommonAction
    {
        [Export]
        public NodePath Area2DPath;

        public override void Initialize(CommonActionData commonActionData)
        {
            base.Initialize(commonActionData);
            var area2d = GetNode<Area2D>(Area2DPath);
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;
            var worldActionData = GetActionData<WorldActionData>();
            shape.Radius = worldActionData.Radius;

            area2d.Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
        }

        protected virtual void OnBodyEntered(Node body) { }
    }
}

