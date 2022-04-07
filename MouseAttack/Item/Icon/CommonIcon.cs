using Godot;
using MouseAttack.Constants;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Icon
{
    public class CommonIcon : PanelContainer, IItemView
    {
        TextureRect TextureRect => GetNode<TextureRect>(nameof(TextureRect));

        const float ColorAlpha = 0.5f;
        async virtual public void SetItem(CommonItem item)
        {
            await ToSignal(this, Signals.Ready);

            TextureRect.Texture = item.GetIconTexture();
            StyleBoxFlat styleBox = Get(Overrides.CustomStylesPanel) as StyleBoxFlat;
            styleBox.BorderColor = item.Color.WithAlpha(ColorAlpha);
            styleBox.BgColor = item.Color.WithAlpha(ColorAlpha).Contrasted();
        }
    }
}
