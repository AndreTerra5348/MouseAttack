using Godot;

namespace MouseAttack.Action
{
    /// <summary>
    /// Base class for Collidable Action
    /// </summary>
    public abstract class CollidableAction : CommonAction
    {
        [Export]
        public float Radius { get; private set; } = 10;
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionLayer { get; private set; }
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionMask { get; private set; }

        public override string Tooltip
        {
            get
            {
                return
                    $"Radius: {Radius.ToString("0.0")}\n" + 
                    base.Tooltip;
            }
        }
    }
}
