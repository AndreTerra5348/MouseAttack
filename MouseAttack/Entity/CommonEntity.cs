using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;

namespace MouseAttack.Entity
{
    public abstract class CommonEntity : KinematicBody2D
    {
        public override void _EnterTree()
        {
            this.MakeResourcesCopy(typeof(CommonEntity));
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
        }

        sealed public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
        {
            if (@event.IsActionPressed("RMB"))
                OnRightMouseButtonClicked();
        }

        protected virtual void OnMouseEntered()
        {
            ToggleHoverFeedback();
        }

        protected virtual void OnMouseExited()
        {
            ToggleHoverFeedback();
        }

        protected abstract void ToggleHoverFeedback();
        
        protected virtual void OnRightMouseButtonClicked()
        {
        }
    }
}

