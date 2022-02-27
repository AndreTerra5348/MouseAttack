using Godot;
using MouseAttack.Character;
using MouseAttack.Extensions;
using MouseAttack.World.Autoload;
using System;

namespace MouseAttack.Entity.Monster
{
    public class CommonMonster : CommonAliveEntity
    {
        [Export]
        StatsData _movementSpeed;
        [Export]
        StatsData _damage;
        [Export]
        StatsData _defense;

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
            GD.PrintT("Enemy Health:", Health, GetInstanceId());
            // Hit feedback
        }

        protected override void OnMouseEntered()
        {
            GD.PrintT("OnMouseEntered CommonMonster");
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

