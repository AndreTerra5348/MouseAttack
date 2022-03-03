using Godot;
using MouseAttack.Action.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Entity.Monster
{
    public class MonsterEntity : SpecializedEntity<MonsterCharacter>
    {
        protected override string CharacterName => nameof(MonsterCharacter);
        public CastleAttacker CastleAttacker { get; private set; }
        public CastleDetector CastleDetector { get; private set; }
        public bool IsDead => Character.IsDead;

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());

        public override void _Ready()
        {            
            base._Ready();
            CastleDetector = GetNode<CastleDetector>(nameof(CastleDetector));
            // Stop moving when castle is in range
            CastleDetector.Detected += (object sender, CastleDetectedEventArgs e) => SetProcess(false);
            CastleDetector.Lost += (object sender, EventArgs e) => SetProcess(true);

            CastleAttacker = GetNode<CastleAttacker>(nameof(CastleAttacker));
            CastleAttacker.SetMonsterEntity(this);
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            if (Character.MovementSpeed.Value <= 0)
                return;
            Position = Position.MoveToward(Stage.PlayerEntity.Position, Character.MovementSpeed.Value);
        }

        async protected override void OnDeath(object sender, EventArgs e)
        {
            // death animation

            // TODO: change to await for the animation to finish
            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timer.Timeout);
            QueueFree();
        }

        protected override void OnMouseEntered()
        {
            // Toggle Hover Feedback
        }

        protected override void OnMouseExited()
        {
            // Toggle Hover Feedback
        }
    }
}

