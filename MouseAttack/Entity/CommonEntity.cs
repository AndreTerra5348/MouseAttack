using Godot;
using MouseAttack.Extensions;
using System;

namespace MouseAttack.Entity
{
    public class CommonEntity : KinematicBody2D
    {
        public override void _EnterTree()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
        }      

        sealed public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
        {
            if (@event.IsActionPressed("RMB"))
                OnRightMouseButtonClicked();
            if (@event is InputEventMouseMotion)
                OnMouseHover();
        }

        protected virtual void OnMouseHover()
        {
        }

        protected virtual void OnMouseEntered()
        {
        }

        protected virtual void OnMouseExited()
        {
        }
        
        protected virtual void OnRightMouseButtonClicked()
        {
        }
    }
}

