using Godot;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.Data
{
    public abstract class CollidableSkillFactory : CommonSkillFactory
    {
        [AssignTo(nameof(CollidableSkill.Area))]
        [Export]
        public Vector2 Area { get; private set; } = Vector2.One;
        [AssignTo(nameof(CollidableSkill.CollisionLayer))]
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionLayer { get; private set; }
        [AssignTo(nameof(CollidableSkill.CollisionMask))]
        [Export(PropertyHint.Layers2dPhysics)]
        public uint CollisionMask { get; private set; }
    }
}
