using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public class SharableNode : Node, ISharable
    {
        public SharableNode() =>
            TreeSharer.RegistryNode(this);
    }
}
