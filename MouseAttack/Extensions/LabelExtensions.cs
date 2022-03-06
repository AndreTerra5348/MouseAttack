using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class LabelExtensions
    {
        const string FontColorPropertyName = "font_color";
        public static void SetFontColor(this Label label, Color color) =>
            label.AddColorOverride(FontColorPropertyName, color);
    }
}
