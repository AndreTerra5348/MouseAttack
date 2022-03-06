using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;

namespace MouseAttack.Entity.Player
{
    public class PlayerEntity : SpecializedEntity<PlayerCharacter>
    {
        protected override string CharacterName => nameof(PlayerCharacter);   

        public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx)
        {
            if (@event.IsActionPressed("RMB"))
                OnRightMouseButtonClicked();
        }

        private void OnRightMouseButtonClicked()
        {
            // Menu
        }

        protected override void OnDeath(object sender, EventArgs e)
        {
            // Game Over
        }

        protected override void OnMouseEntered()
        {
            // Toggle Hover Feedback
        }

        protected override void OnMouseExited()
        {
            // Toggle Hover Feedback
        }
    }
}
