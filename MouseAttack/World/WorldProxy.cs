using Godot;
using System.Collections.Generic;

namespace MouseAttack.World
{
    public class WorldProxy : Node
    {
        Node2D world;
        Queue<Node2D> orphans = new Queue<Node2D>();

        public void RegistryGame(Game game)
        {
            world = game.GetWorld();
            while (orphans.Count > 0)
            {
                var child = orphans.Dequeue();
                world.AddChild(child);
            }
        }

        public void AddChild(Node2D child)
        {
            if (world == null)
            {
                orphans.Enqueue(child);
                return;
            }
            world.AddChild(child);
        }
    }
}

