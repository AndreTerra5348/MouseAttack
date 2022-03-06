using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.MonsterSystem
{
    public class SpawnPoint : Path2D
    {
        Random _random = new Random();
        PathFollow2D _pathFollow2d;

        public override void _Ready()
        {
            base._Ready();
            _pathFollow2d = GetNode<PathFollow2D>(nameof(PathFollow2D);
        }

        public Vector2 GetRandomPosition()
        {
            _pathFollow2d.Offset = _random.Next();
            return _pathFollow2d.Position.Snapped(Vector2.One);
        }
    }
}
