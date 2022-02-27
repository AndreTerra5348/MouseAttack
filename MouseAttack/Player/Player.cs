using Godot;
using MouseAttack.Character;
using MouseAttack.Misc;
using System;

namespace MouseAttack.Player
{
    public class Player : Node2D
    {
        [Export]
        [MakeUnique]
        StatsData _damage;
    }
}