using Godot;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World
{
    public class DragPreviewParent : Control, ISharable
    {
        public DragPreviewParent() =>
            TreeSharer.RegistryNode(this);
    }
}
