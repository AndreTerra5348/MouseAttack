using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class ControlExntesions
    {
        public static Control MakeCustomTooltip(this Control control, string forText)
        {
            Control ctrl = new Control();
            CanvasLayer canvasLayer = new CanvasLayer();
            Label label = new Label();

            ctrl.AddChild(canvasLayer);
            canvasLayer.AddChild(label);

            label.Text = forText;
            return ctrl;
        }
    }
}
