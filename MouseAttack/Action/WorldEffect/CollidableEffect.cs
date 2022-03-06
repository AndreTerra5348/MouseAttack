using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Action.WorldEffect
{
    public abstract class CollidableEffect : CommonEffect
    {
        new CollidableAction Action => base.Action as CollidableAction;
        public override void _Ready()
        {
            base._Ready();
            var area2d = GetNode<Area2D>(nameof(Area2D));
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;            
            shape.Radius = Action.Radius;
            area2d.Connect(Signals.AreaEntered, this, nameof(OnAreaEntered));
            area2d.CollisionLayer = Action.CollisionLayer;
            area2d.CollisionMask = Action.CollisionMask;            
        }

        protected virtual void OnAreaEntered(Area2D area)
        {
            CommonEntity target = area as CommonEntity;
            if (target == null)
                return;
            OnCollision(target);
            if (QueueFreeTimerStarter == QueueFreeTimerStarter.AfterCollision)
                QueueFreeTimer.Start();
        }

        protected abstract void OnCollision(CommonEntity target);
    }
}

