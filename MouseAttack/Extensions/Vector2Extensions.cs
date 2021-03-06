using Godot;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 SnappedDown(this Vector2 v, Vector2 step) =>
            new Vector2((int)(v.x / step.x) * step.x, (int)(v.y / step.y) * step.y);

        public static IntegerRange ToIntegerRange(this Vector2 vector2) => 
            new IntegerRange((int)vector2.x, (int)vector2.y);
    }
}
