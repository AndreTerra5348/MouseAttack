using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabelProvider : SharableNode
    {
        [Export]
        PackedScene FloatingLabelScene { get; set; }

        public FloatingLabel GetLabel() =>
            FloatingLabelScene.Instance<FloatingLabel>();
    }
}
