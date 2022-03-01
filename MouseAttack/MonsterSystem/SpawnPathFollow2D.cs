using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World
{
    public class SpawnPathFollow2D : PathFollow2D
    {
        Random _random = new Random();
        public Vector2 RandomPosition
        {
            get
            {
                Offset = _random.Next();
                return Position;
            }
        }
    }
}
