using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Item.Data;
using MouseAttack.Item.Tooltip;
using System.Collections.Generic;

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

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Area: {Area.x.ToString("0")}x{Area.y.ToString("0")}", Colors.GreenYellow));
            return tooltipInfo;
        }

        public abstract void Apply(CommonEntity user, CommonEntity target);
    }
}
