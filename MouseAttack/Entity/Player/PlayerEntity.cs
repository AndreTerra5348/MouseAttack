using Godot;
using MouseAttack.Characteristic;
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

        public PlayerEntity() => TreeSharer.RegistryNode(this);

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
