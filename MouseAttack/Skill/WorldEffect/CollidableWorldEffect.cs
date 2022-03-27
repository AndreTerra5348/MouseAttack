using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Skill.WorldEffect
{
    public abstract class CollidableWorldEffect : CommonWorldEffect
    {
        new CollidableSkill Skill => base.Skill as CollidableSkill;
        public override void _Ready()
        {
            base._Ready();
            var area2d = GetNode<Area2D>(nameof(Area2D));
            // Update collision shape and position            
            //var shape = area2d.ShapeOwnerGetShape(0, 0) as RectangleShape2D;
            //shape.Extents = Vector2.One * Skill.Area * 32;

            area2d.Connect(Signals.AreaEntered, this, nameof(OnAreaEntered));
            area2d.CollisionLayer = Skill.CollisionLayer;
            area2d.CollisionMask = Skill.CollisionMask;
        }

        protected virtual void OnAreaEntered(Area2D area)
        {
            CommonEntity target = area as CommonEntity;
            if (target == null)
                return;
            OnCollision(target);
        }

        protected abstract void OnCollision(CommonEntity target);

    }
}
