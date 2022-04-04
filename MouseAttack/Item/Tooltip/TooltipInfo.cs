using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Tooltip
{
    public struct TooltipInfo
    {
        public string Text { get; private set; }
        public Color Color { get; private set; }
        public TooltipInfo(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
}
