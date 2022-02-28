using Godot;
using MouseAttack.Character;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Castle
{
    public class CastleEntity : CommonAliveEntity
    {
        [Export]
        [MakeUnique]
        StatsData _healthRegen = null;

        public override void _Ready()
        {
            base._Ready();
            var worldProxy = this.GetAutoload<WorldProxy>();
            worldProxy.RegistryCastle(this);
            AddChild(new ResourceRegenerator(this));
        }

        protected override void OnRightMouseButtonClicked()
        {
            // Castle Menu
        }

        protected override void OnDeath()
        {
            // Game Over
        }

        protected override void OnHit()
        {
            // Hit feedback
        }

        protected override void ToggleHoverFeedback()
        {
            // Hover Feeback
        }

        public override void Regenerate()
        {
            Regenerate(_healthRegen.Value);
        }
    }
}
