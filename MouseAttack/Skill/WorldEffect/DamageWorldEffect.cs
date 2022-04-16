using Godot;
using MouseAttack.Entity;
using MouseAttack.Skill.Data;
using System.Collections.Generic;
using MouseAttack.Skill.TargetEffect;

namespace MouseAttack.Skill.WorldEffect
{
    public class DamageWorldEffect : CollidableWorldEffect
    {
        [Export]
        NodePath SpriteNodePath { get; set; }
        public Sprite Sprite { get; private set; }

        public override void _Ready()
        {
            base._Ready();
            Sprite = GetNode<Sprite>(SpriteNodePath);
        }
            
    }
}
