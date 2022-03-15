using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ActionDragPreview : Control
    {
        public ActionDragPreview() { }
        public ActionDragPreview(TextureRect textureRect)
        {
            ColorRect colorRect = new ColorRect();
            AddChild(colorRect);
            colorRect.AddChild(textureRect);
            textureRect.RectPosition = Vector2.Zero;
            colorRect.Color = new Color("504f4f");
            colorRect.RectSize = new Vector2(30, 30);
            colorRect.RectPosition = new Vector2(-15, -15);
        }
    }
}
