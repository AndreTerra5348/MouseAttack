using Godot;
using MouseAttack.Entity;

namespace MouseAttack.Action
{
    public class CommonEffect : Node2D
    {
        public CommonAction CommonAction { get; set; }
        public Character Character { get; set; }
    }
}
