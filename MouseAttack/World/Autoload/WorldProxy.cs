using Godot;
using MouseAttack.Entity.Castle;
using System.Collections.Generic;

namespace MouseAttack.World.Autoload
{
    public class WorldProxy : Node
    {
        Node2D _world;
        Node2D _castle;

        public Vector2 CastlePosition { get => _castle.Position; }
        public void RegistryGame(Game game) => _world = game.GetWorld();
        public void RegistryCastle(Castle castle) => _castle = castle;
        public void AddChild(Node2D child) => _world.AddChild(child);
        public void AddChildAtMousePosition(Node2D child)
        {
            _world.AddChild(child);
            child.Position = GetViewport().GetMousePosition();
        }
    }
}

