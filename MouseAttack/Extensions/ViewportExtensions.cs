using Godot;
using MouseAttack.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class ViewportExtensions
    {
        public static Vector2 GetSnappedMousePosition(this Viewport viewport, Vector2 step) =>
            viewport.GetMousePosition().SnappedDown(step);
    }
}
