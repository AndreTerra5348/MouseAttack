using Godot;
using MouseAttack.Action.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Entity.Monster
{
    public class MonsterEntity : KinematicBody2D
    {
        public event EventHandler Freed;
        public MonsterCharacter Character { get; private set; }
        public CastleAttacker CastleAttacker { get; private set; }
        public CastleDetector CastleDetector { get; private set; }
        public bool IsDead => Character.IsResourceEmpty;        

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());

        public override void _EnterTree()
        {
            Connect(Signals.CollisionObject2D.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.CollisionObject2D.MouseExited, this, nameof(OnMouseExited));
        }

        public override void _Ready()
        {
            CastleAttacker = GetNode<CastleAttacker>(nameof(CastleAttacker));
            CastleDetector = GetNode<CastleDetector>(nameof(CastleDetector));
            CastleDetector.Detected += (object sender, CastleDetectedEventArgs e) => SetPhysicsProcess(false);
            CastleDetector.Lost += (object sender, EventArgs e) => SetPhysicsProcess(true);
            Character = GetNode<MonsterCharacter>(nameof(MonsterCharacter));
            Character.Dead += OnDeath;
            CastleAttacker.SetMonsterEntity(this);
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Character.MovementSpeed.Value == 0)
                return;
            var _castleDirection = Position.DirectionTo(Stage.CastleEntity.Position);
            LookAt(Stage.CastleEntity.Position);
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

