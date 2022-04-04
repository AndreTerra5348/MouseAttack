using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class ColorExtensions
    {
        public static Color WithAlpha(this Color color, float a) =>
            new Color(color.r, color.g, color.b, a);
    }
}
