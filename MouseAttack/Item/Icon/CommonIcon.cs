using Godot;
using MouseAttack.Constants;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Icon
{
    public class CommonIcon : PanelContainer
    {
        TextureRect TextureRect => GetNode<TextureRect>(nameof(TextureRect));
        StyleBoxFlat StyleBoxFlat => Get(Overrides.CustomStylesPanel) as StyleBoxFlat;
        public CommonItem Item { get; set; }
        public IconColorInfo ColorInfo { get; set; }

        public override void _Ready()
        {
            TextureRect.Texture = Item.GetIconTexture();
            StyleBoxFlat styleBox = StyleBoxFlat;
            styleBox.BorderColor = ColorInfo.Border;
            styleBox.BgColor = ColorInfo.Background;
        }
        
    }
}
