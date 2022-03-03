using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using System;
using System.Collections.Generic;

namespace MouseAttack.Entity.Castle
{
    public class CastleEntity : Area2D
    {
        public CastleCharacter Character { get; private set; }

        public override void _EnterTree()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
            Connect(Signals.Area2D.BodyEntered, this, nameof(OnBodyEntered));
        }        

        public override void _Ready()
        {
            base._Ready();
            Character = GetNode<CastleCharacter>(nameof(CastleCharacter));
            Character.Dead += OnDeath;
        }
        

        sealed public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
        {
            if (@event.IsActionPressed("RMB"))
                OnRightMouseButtonClicked();
        }

        private void OnMouseEntered()
        {
            // Toggle Hover Feeback
        }

        private void OnMouseExited()
        {
            // Toggle Hover Feeback
        }

        private void OnRightMouseButtonClicked()
        {
            // Castle Menu
        }

        private void OnDeath(object sender, EventArgs e)
        {
            // Game Over
        }

        private void OnBodyEntered(Node body)
        {

        }
    }
}
