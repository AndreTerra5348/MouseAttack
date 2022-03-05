using Godot;
using MouseAttack.Action.Monster;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;

namespace MouseAttack.Entity.Monster
{
    public class MonsterEntity : SpecializedEntity<MonsterCharacter>, IInitializable
    {
        public event EventHandler Initialized;
        public event EventHandler Freed;
        protected override string CharacterName => nameof(MonsterCharacter);
        public PlayerAttacker PlayerAttacker { get; private set; }
        public PlayerDetector PlayerDetector { get; private set; }
        public bool IsDead => Character.IsDead;

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());


        async public override void _Ready()
        {            
            base._Ready();
            PlayerDetector = GetNode<PlayerDetector>(nameof(PlayerDetector));
            PlayerAttacker = GetNode<PlayerAttacker>(nameof(PlayerAttacker));
            Initialized?.Invoke(this, EventArgs.Empty);

            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timer.Timeout);

            var sprite = GetNode<Sprite>(nameof(Sprite));
            sprite.FlipH = GlobalPosition.DirectionTo(Stage.PlayerEntity.GlobalPosition).x > 0;
        }

        protected override void OnDeath(object sender, EventArgs e)
        {
            Freed?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseEntered()
        {

        }

        protected override void OnMouseExited()
        {

        }
    }
}

