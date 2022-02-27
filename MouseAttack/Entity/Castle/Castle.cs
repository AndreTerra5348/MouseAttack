using Godot;
using MouseAttack.Extensions;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Castle
{
    public class Castle : CommonAliveEntity
    {
        public override void _Ready()
        {
            base._Ready();
            var worldProxy = this.GetAutoload<WorldProxy>();
            worldProxy.RegistryCastle(this);
        }

        protected override void OnRightMouseButtonClicked()
        {
        }

        protected override void OnMouseEntered()
        {
            // Enable Hover Feeback
        }

        protected override void OnMouseExited()
        {
            // Disable Hover Feedback
        }

        protected override void OnDeath()
        {
            // Game Over
        }

        protected override void OnHit()
        {
            // Hit feedback
        }
    }
}
