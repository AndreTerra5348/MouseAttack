using Godot;
using MouseAttack.Constants;
using MouseAttack.Item;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class DropLabel : FloatingLabel
    {
        [Export]
        NodePath IconPath { get; set; }
        public Texture IconTexture { get; set; }

        public override void _Ready()
        {
            base._Ready();
            TextureRect icon = GetNode<TextureRect>(IconPath);
            icon.Texture = IconTexture;
        }
    }
}
