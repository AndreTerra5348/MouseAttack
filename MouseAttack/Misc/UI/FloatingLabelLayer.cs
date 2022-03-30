using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabelLayer : CanvasLayer, ISharable
    {
        public FloatingLabelLayer() =>
            TreeSharer.RegistryNode(this);
    }
}
