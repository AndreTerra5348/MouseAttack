using Godot;

namespace MouseAttack.Action
{
    public class WorldAction : CommonAction
    {
        [Export]
        public NodePath Area2DPath;

        public override void SetData(CommonActionData commonActionData)
        {
            base.SetData(commonActionData);

            var worldActionData = commonActionData as WorldActionData;
            var area2d = GetNode<Area2D>(Area2DPath);
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;
            shape.Radius = worldActionData.Radius;

            area2d.Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
        }

        protected virtual void OnBodyEntered(Node body)
        {
            
        }
    }
}

