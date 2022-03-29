using Godot;

namespace MouseAttack.Skill.Data
{
    /// <summary>
    /// Base class for Collidable Skill
    /// </summary>
    public abstract class CollidableSkill : CommonSkill
    {
        [Export]
        public int Area { get; private set; } = 1;
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionLayer { get; private set; }
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionMask { get; private set; }

        public override string Tooltip =>
            $"Area: {Area}\n{base.Tooltip}";
    }
}
