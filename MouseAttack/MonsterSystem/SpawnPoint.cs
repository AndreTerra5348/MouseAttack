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
        PathFollow2D PathFollow2D => _pathFollow2d ?? (_pathFollow2d = GetNode<PathFollow2D>(nameof(Godot.PathFollow2D)));

        public Vector2 RandomPosition
        {
            get
            {
                PathFollow2D.Offset = _random.Next();
                return PathFollow2D.Position.Snapped(Vector2.One);
            }
        }
    }
}
