using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Entity.Monster
{
    public class MonsterEntity : KinematicBody2D
    {
        // Events
        public event EventHandler Freed;

        public MonsterCharacter Character { get; private set; }

        Stage _stage;

        public override void _EnterTree()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));            
        }

        public override void _Ready()
        {
            _stage = this.GetStage();
            Character = GetNode<MonsterCharacter>(nameof(MonsterCharacter));
            Character.Dead += OnDeath;
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Character.MovementSpeed.Value == 0)
                return;
            var _castleDirection = Position.DirectionTo(_stage.Castle.Position);
            LookAt(_stage.Castle.Position);
            MoveAndCollide(_castleDirection * Character.MovementSpeed.Value);
        }

        private void OnMouseEntered()
        {
            // Toggle Hover feedback
        }

        private void OnMouseExited()
        {
            // Toggle Hover feedback
        }

        async private void OnDeath(object sender, EventArgs e)
        {
            // death animation

            // TODO: change to await for the animation to finish
            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timer.Timeout);
            Freed?.Invoke(this, EventArgs.Empty);
            QueueFree();
        }

        
    }
}

