using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class MonsterEntity : SpecializedEntity<MonsterCharacter>
    {
        public event EventHandler Dead;
        protected override string CharacterName => nameof(MonsterCharacter);
        
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        MonsterSkillController MonsterSkillController { get; set; }
        [Export]
        public int Level { get; private set; } = 1;

        [Export]
        NodePath _spritePath = "";
        Sprite _sprite;
        [Export]
        NodePath _nameLabelPath = "";

        public float MovementDelay { get; private set; } = 0.1f;

        async public override void _Ready()
        {
            base._Ready();
            MonsterSkillController = GetNode<MonsterSkillController>(nameof(MonsterSkillController));
            _sprite = GetNode<Sprite>(_spritePath);
            Label nameLabel = GetNode<Label>(_nameLabelPath);
            nameLabel.Text += $" Lv:{Level}";

            OnEntityInitialized();
            // Skip a frame to update it's position
            await this.SkipNextFrame();

            // Flip Sprite to face player
            _sprite.FlipH = GlobalPosition.DirectionTo(PlayerEntity.GlobalPosition).x > 0;
            GridController.SetCellAsTaken(Position);
        }

        protected override void OnDeath()
        {
            GridController.SetCellAsEmpty(Position);
            Dead?.Invoke(this, EventArgs.Empty);
            QueueFree();
        }

        public override void OnCursorEntered()
        {
        }

        public override void OnCursorExited()
        {
        }

        public override SignalAwaiter Act()
        {
            Vector2[] availablePath = GridController.GetAvailablePath(Position, PlayerEntity.Position);
            if (availablePath.Length == 0)
                return this.CreateTimer(MovementDelay);
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
            return this.CreateTimer(MovementDelay);
        }
    }
}

