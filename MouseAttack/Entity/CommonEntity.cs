using Godot;
using System;

namespace MouseAttack.Entity
{
    public class CommonEntity : KinematicBody2D
    {
        public override void _Ready()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
        }

        protected virtual void OnMouseExited()
        {
            // hover feedback on
        }

        protected virtual void OnMouseEntered()
        {
            // hover feedback off
        }
    }
}

