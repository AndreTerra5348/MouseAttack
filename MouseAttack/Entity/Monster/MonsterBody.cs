using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class MonsterBody : KinematicBody2D
    {
        public MonsterEntity Entity { get; private set; }
        Stage _stage;
        Vector2 _playerPosition;

        public override void _Ready()
        {
            base._Ready();
            Entity = GetNode<MonsterEntity>(nameof(MonsterEntity));
            _stage = this.GetStage();
            _playerPosition = _stage.PlayerEntity.Position;

            // Stop moving when castle is in range
            Entity.PlayerDetector.Detected += (object sender, PlayerDetectedEventArgs e) => SetPhysicsProcess(false);
            Entity.PlayerDetector.Lost += (object sender, EventArgs e) => SetPhysicsProcess(true);

            Entity.Freed += (object sender, EventArgs e) => QueueFree();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Entity.Character.MovementSpeed.Value == 0)
                return;
            var _castleDirection = Position.DirectionTo(_playerPosition);
            MoveAndCollide(_castleDirection * Entity.Character.MovementSpeed.Value);
        }
    }
}
