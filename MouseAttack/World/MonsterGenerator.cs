using Godot;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World.Autoload;
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
        List<PackedScene> _monsters;
        [Export]
        NodePath _spawnPathFollowPath;

        int _currentLevel = 0;
        Random _random = new Random();
        WorldProxy _worldProxy;

        public override void _Ready()
        {
            _worldProxy = this.GetAutoload<WorldProxy>();
            
        }

        public override void _Input(InputEvent @event)
        {
            if(@event.IsActionPressed("ui_accept"))
                for (int i = 0; i < 10; i++)
                    Spawn();
        }

        void Spawn()
        {
            var pathFollow = GetNode<PathFollow2D>(_spawnPathFollowPath);
            pathFollow.Offset = _random.Next();

            var instance = _monsters[_currentLevel].Instance<CommonMonster>();            
            _worldProxy.AddChild(instance);
            instance.Position = pathFollow.Position;
        }

    }
}
