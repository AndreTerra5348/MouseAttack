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

        private const int ShapeExtentsMultiplier = 14;

        new CollidableSkill Skill => base.Skill as CollidableSkill;

        public override void _Ready()
        {
            base._Ready();
            var area2d = GetNode<Area2D>(nameof(Area2D));
            var shape = area2d.ShapeOwnerGetShape(0, 0) as RectangleShape2D;
            shape.Extents = Skill.Area * ShapeExtentsMultiplier;

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

        async protected virtual void OnCollision(CommonEntity target)
        {
            Skill.Apply(User, target);
            if (QueueFreeMode != QueueFreeMode.Collision)
                return;

            await this.CreateTimer(QueueFreeDelay);
            QueueFree();
        }

    }
}

