using Godot;
using System;

using MouseAttack.Extensions;
using MouseAttack.World.Autoload;

namespace MouseAttack
{
    public class Game : Node
    {
        [Export]
        public NodePath WorldPath;

        public override void _Ready()
        {
            var worldProxy = this.GetAutoload<WorldProxy>();
            worldProxy.RegistryGame(this);
        }

        public Node2D GetWorld()
        {
            return GetNode<Node2D>(WorldPath);
        }
    }
}


