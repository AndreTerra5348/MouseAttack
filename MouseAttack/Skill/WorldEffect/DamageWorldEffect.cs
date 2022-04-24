using Godot;
using MouseAttack.Entity;
using MouseAttack.Skill.Data;
using System.Collections.Generic;
using MouseAttack.Skill.TargetEffect;
using MouseAttack.Constants;

namespace MouseAttack.Skill.WorldEffect
{
    public class DamageWorldEffect : CollidableWorldEffect
    {
        new DamageSkill Skill => base.Skill as DamageSkill;
        [Export]
        NodePath SpriteNodePath { get; set; }
        public Sprite Sprite { get; private set; }


        public override void _Ready()
        {
            base._Ready();
            Sprite = GetNode<Sprite>(SpriteNodePath);
            Sprite.RegionRect = new Rect2(Vector2.Zero, Skill.Area * Values.CellSize);
        }
    }
}
