using Godot;
using MouseAttack.Item.Data;

namespace MouseAttack.Skill.Data
{
    /// <summary>
    /// Base class for Collidable Skill
    /// </summary>
    public abstract class CollidableSkill : CommonSkill
    {
        public Vector2 Area { get; private set; }
        public uint CollisionLayer { get; private set; }
        public uint CollisionMask { get; private set; }

        public override string Tooltip =>
            $"Area: {Area.x.ToString("0")}x{Area.y.ToString("0")}\n{base.Tooltip}";
    }
}
