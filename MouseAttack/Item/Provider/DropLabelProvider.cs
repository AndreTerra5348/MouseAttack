using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Provider
{
    public class DropLabelProvider : SharableNode
    {
        [Export]
        PackedScene DropLabelScene { get; set; }

        public FloatingLabel GetDropLabel(CommonItem item)
        {
            DropLabel dropLabel = DropLabelScene.Instance<DropLabel>();
            dropLabel.IconTexture = item.GetIconTexture();
            dropLabel.Text = item.DropText;
            dropLabel.Color = item.Color;
            return dropLabel;
        }
        
    }
}
