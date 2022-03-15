using Godot;
using MouseAttack.Constants;
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
        public bool IsDead => Character.IsDead;

        Stage _stage;


        async public override void _Ready()
        {            
            base._Ready();
            _stage = this.GetStage();

            Initialized?.Invoke(this, EventArgs.Empty);

            // Flip Sprite to face player
            await ToSignal(GetTree().CreateTimer(0.5f), Signals.Timeout);
            var sprite = GetNode<Sprite>(nameof(Sprite));
            sprite.FlipH = GlobalPosition.DirectionTo(_stage.PlayerEntity.GlobalPosition).x > 0;            
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

