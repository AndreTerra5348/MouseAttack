using Godot;
using MouseAttack.Character;
using MouseAttack.Entity.Castle;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;

namespace MouseAttack.Entity.Monster
{
    public class CommonMonster : CommonAliveEntity
    {
        public event EventHandler Freed;
        [Export]
        [MakeCopy]
        public Stats MovementSpeed { get; private set; }
        [Export]
        [MakeCopy]
        public Stats Damage { get; private set; }
        [Export]
        [MakeCopy]
        public Stats Defense { get; private set; }

        Stage _stage;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
        }

        public override void _PhysicsProcess(float delta)
        {
            var _castleDirection = Position.DirectionTo(_stage.Castle.Position);
            LookAt(_stage.Castle.Position);
            MoveAndCollide(_castleDirection * MovementSpeed.Value);
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
            Freed?.Invoke(this, EventArgs.Empty);
            QueueFree();
        }
        
    }
}

