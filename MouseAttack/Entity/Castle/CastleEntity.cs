using Godot;
using MouseAttack.Character;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using System;

namespace MouseAttack.Entity.Castle
{
    public class CastleEntity : CommonAliveEntity
    {
        [Export]
        [MakeCopy]
        public StatsData HealthRegen { get; private set; }

        public override void _Ready()
        {
            base._Ready();
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
            Regenerate(HealthRegen.Value);
        }
    }
}
