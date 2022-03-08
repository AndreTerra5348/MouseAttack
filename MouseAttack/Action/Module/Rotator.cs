using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action.Module
{
    public class Rotator : Node
    {
        Random _random = new Random();
        public override void _Ready()
        {
            GetParent<Node2D>().Rotation = _random.Next();
        }
    }
}
