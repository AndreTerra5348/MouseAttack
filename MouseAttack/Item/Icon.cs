using Godot;
using MouseAttack.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item
{
    public class Icon : PanelContainer
    {
        public Texture Texture { get; set; }
        [Export]
        public Color BorderColor { get; set; }
        [Export]
        public Color BgColor { get; set; }
        public override void _Ready()
        {
            TextureRect textureRect = GetNode<TextureRect>(nameof(TextureRect));
            textureRect.Texture = Texture;
            StyleBoxFlat styleBox = Get(Overrides.CustomStylesPanel) as StyleBoxFlat;
            styleBox.BorderColor = BorderColor;
            styleBox.BgColor = BgColor;
        }

    }
}
