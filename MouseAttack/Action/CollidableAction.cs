using Godot;

namespace MouseAttack.Action
{
    public class CollidableAction : CommonAction
    {
        [Export]
        public float Radius { get; private set; } = 10;
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionLayer { get; private set; }
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionMask { get; private set; }
    }
}
