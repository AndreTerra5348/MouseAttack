using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;

namespace MouseAttack.Entity.Player
{
    public class PlayerEntity : SpecializedEntity<PlayerCharacter>, ISharable
    {
        protected override string CharacterName => nameof(PlayerCharacter);

        public PlayerEntity() => 
            TreeSharer.RegistryNode(this);

        public override void _Ready()
        {
            base._Ready();
            OnEntityInitialized();
        }

        public override void _Input(InputEvent @event)
        {
            if (GridController.IsTurnElapsing)
                return;
            if (@event.IsActionPressed(InputAction.ElapseTurn))
                GridController.ElapseTurn();
        }

        protected override void OnDeath()
        {
            // Game Over
        }

        public override void OnCursorEntered()
        {
            // Toggle Hover Feedback
        }

        public override void OnCursorExited()
        {
            // Toggle Hover Feedback
        }

        public override SignalAwaiter Act()
        {
            return null;
        }
    }
}
