using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Misc;
using MouseAttack.World;
using System;

namespace MouseAttack.Characteristic.System
{
    public class ResourceRegenerator : Node
    {
        [Export]
        NodePath _regenPath = "";

        public override void _Ready()
        {
            ResourcePool resource = GetParent<ResourcePool>();
            Stats regen = GetNode<Stats>(_regenPath);
            TreeSharer.GetNode<GridController>().RoundFinished += (s, e) =>
            {
                if (!resource.IsFull)
                    resource.Regenerate(regen.Value);
            };
        }
    }
}
