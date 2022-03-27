using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class DragPreview : Control
    {
        public DragPreview() { }
        public DragPreview(Control icon)
        {
            Control control = new Control();
            AddChild(control);
            control.AddChild(icon);
            control.RectSize = new Vector2(30, 30);
            control.RectPosition = new Vector2(-15, -15);
        }
    }
}
