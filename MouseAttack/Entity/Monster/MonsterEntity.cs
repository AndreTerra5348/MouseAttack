using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
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

        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        MonsterSkillController MonsterSkillController { get; set; }
        [Export]
        NodePath _spritePath = "";
        Sprite _sprite;

        float _movementDelay = 0.1f;

        async public override void _Ready()
        {
            base._Ready();
            MonsterSkillController = GetNode<MonsterSkillController>(nameof(MonsterSkillController));
            _sprite = GetNode<Sprite>(_spritePath);
            Initialized?.Invoke(this, EventArgs.Empty);

            // Skip a frame to update it's position
            await this.SkipNextFrame();

            // Flip Sprite to face player
            _sprite.FlipH = GlobalPosition.DirectionTo(PlayerEntity.GlobalPosition).x > 0;
            GridController.SetCellAsTaken(Position);
        }

        protected override void OnDeath(object sender, EventArgs e)
        {
            GridController.SetCellAsEmpty(Position);
            Freed?.Invoke(this, EventArgs.Empty);
            QueueFree();
        }

        protected override void OnMouseEntered()
        {

        }

        protected override void OnMouseExited()
        {

        }

        public override SignalAwaiter Act()
        {
            Vector2[] availablePath = GridController.GetAvailablePath(Position, PlayerEntity.Position);
            if (availablePath.Length == 0)
                return this.CreateTimer(_movementDelay);
            if (availablePath.Length <= MonsterSkillController.AttackRange)            
                return MonsterSkillController.Attack();
            return Move(availablePath);
        }

        private SignalAwaiter Move(Vector2[] availablePath)
        {
            GridController.SetCellAsEmpty(Position);
            // First Point is the current position
            Vector2 nextPoint = availablePath[1];
            GridController.SetCellAsTaken(nextPoint);
            Position = nextPoint;
            _sprite.FlipH = GlobalPosition.DirectionTo(PlayerEntity.GlobalPosition).x > 0;
            return this.CreateTimer(_movementDelay);
        }
    }
}

