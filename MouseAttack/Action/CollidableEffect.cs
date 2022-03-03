using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Action
{
    public class CollidableEffect : CommonEffect
    {
        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());
        public override void _Ready()
        {
            base._Ready();
            var area2d = GetNode<Area2D>(nameof(Area2D));
            var shape = area2d.ShapeOwnerGetShape(0, 0) as CircleShape2D;
            var collidableAction = CommonAction as CollidableAction;
            shape.Radius = collidableAction.Radius;

            area2d.Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
            area2d.Connect(Signals.Area2D.AreaEntered, this, nameof(OnAreaEntered));
            area2d.CollisionLayer = collidableAction.CollisionLayer;
            area2d.CollisionMask = collidableAction.CollisionMask;
        }

        protected virtual void OnAreaEntered(Area2D area)
        {
        }

        protected virtual void OnBodyEntered(Node body)
        {            
        }
    }
}

