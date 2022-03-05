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
        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());
        MonsterEntity _entity;
        public MonsterEntity Entity => 
            _entity ?? (_entity = GetNode<MonsterEntity>(nameof(MonsterEntity)));
        float Speed => Entity.Character.MovementSpeed.Value;
        Vector2 PlayerPosition => Stage.PlayerEntity.Position;

        public override void _Ready()
        {
            // Stop moving when castle is in range
            Entity.PlayerDetector.Detected += (object sender, PlayerDetectedEventArgs e) => SetPhysicsProcess(false);
            Entity.PlayerDetector.Lost += (object sender, EventArgs e) => SetPhysicsProcess(true);

            Entity.Freed += (object sender, EventArgs e) => QueueFree();
        }

        public override void _PhysicsProcess(float delta)
        {
            if (Speed == 0)
                return;
            var _castleDirection = Position.DirectionTo(PlayerPosition);
            MoveAndCollide(_castleDirection * Speed);
        }
    }
}
