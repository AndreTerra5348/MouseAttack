using Godot;
using MouseAttack.Extensions;
using System;

namespace MouseAttack.Entity
{
    public class CommonEntity : KinematicBody2D
    {
        // Sealed avoid not being called from inherited class
        sealed public override void _EnterTree()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
        }

        public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
        {
            if (!@event.IsActionPressed("RMB"))
                return;

            OnRightMouseButtonClicked();
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

