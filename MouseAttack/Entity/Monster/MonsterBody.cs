using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
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

        [Export]
        NodePath _monsterActionControllerPath = "";
        Stage _stage;
        Vector2 _playerPosition;

        public override void _Ready()
        {
            base._Ready();
            //ZIndex = ZOrder.WorldEffect;
            Entity = GetNode<MonsterEntity>(nameof(MonsterEntity));
            Entity.Freed += (s, e) => QueueFree();

            _stage = this.GetStage();
            _playerPosition = _stage.PlayerEntity.Position;

            // Stop moving when castle is in range
            MonsterActionController monsterActionController = GetNode<MonsterActionController>(_monsterActionControllerPath);
            monsterActionController.Detected += (s, e) => SetPhysicsProcess(false);
            monsterActionController.Lost += (s, e) => SetPhysicsProcess(true);
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
