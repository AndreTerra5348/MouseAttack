﻿using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.Module
{
    public class Rotator : Node
    {
        Stage Stage => TreeSharer.GetNode<Stage>();
        public override void _Ready()
        {
            GetParent<Node2D>().Rotation = Stage.Random.Next();
        }
    }
}
