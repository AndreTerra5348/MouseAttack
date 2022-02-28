using Godot;
using MouseAttack.Entity.Castle;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World
{
    public class MonsterGenerator : Node2D
    {
        [Export]
        List<PackedScene> _monsters = null;
        [Export]
        NodePath _spawnPathFollowPath = null;

        int _currentLevel = 0;
        Random _random = new Random();
        PathFollow2D _pathFollow2d = null;
        Stage _stage = null;

        public override void _Ready()
        {
            _pathFollow2d = GetNode<PathFollow2D>(_spawnPathFollowPath);
            _stage = this.GetStage();
        }

        public override void _Input(InputEvent @event)
        {
            if(@event.IsActionPressed("ui_accept"))
                for (int i = 0; i < 10; i++)
                    Spawn();
        }

        void Spawn()
        {
            _pathFollow2d.Offset = _random.Next();

            var instance = _monsters[_currentLevel].Instance<CommonMonster>();
            _stage.AddChild(instance);
            instance.Position = _pathFollow2d.Position;
        }

    }
}
