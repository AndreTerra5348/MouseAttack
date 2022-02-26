using Godot;

namespace MouseAttack.Interaction
{
    public class WorldAction : CommonAction
    {
        [Export]
        public NodePath Area2DPath;

        sealed protected override void OnCommonActionInit()
        {
            var worldActionData = GetActionData<WorldActionData>();
            var area2d = GetNode<Area2D>(Area2DPath);
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;
            shape.Radius = worldActionData.Radius;

            area2d.Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
            OnWorldActionInit();
        }

        protected virtual void OnWorldActionInit() { }

        protected virtual void OnBodyEntered(Node body)
        {
            
        }
    }
}

