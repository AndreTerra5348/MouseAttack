using Godot;
using MouseAttack.Character;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Monster
{
    public class CommonMonster : CommonAliveEntity
    {
        [Export]
        [MakeUnique]
        StatsData _movementSpeed = null;
        [Export]
        [MakeUnique]
        StatsData _damage = null;
        [Export]
        [MakeUnique]
        StatsData _defense = null;

        WorldProxy _worldProxy;

        public override void _Ready()
        {
            base._Ready();
            _worldProxy = this.GetAutoload<WorldProxy>();
        }

        public override void _PhysicsProcess(float delta)
        {
            var _castleDirection = Position.DirectionTo(_worldProxy.CastlePosition);
            LookAt(_worldProxy.CastlePosition);
            MoveAndCollide(_castleDirection * _movementSpeed.Value);
        }

        protected override void OnHit()
        {
            // Hit feedback
        }

        protected override void ToggleHoverFeedback()
        {
            // Hover feedback
        }

        async protected override void OnDeath()
        {
            // death animation

            // TODO: change to await for the animation to finish
            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timer.Timeout);
            QueueFree();
        }
        
    }
}

