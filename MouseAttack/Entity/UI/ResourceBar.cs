using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class ResourceBar : ProgressBar
    {
        [Export]
        NodePath _resourcePath = "";
        public override void _Ready()
        {
            base._Ready();
            ResourcePool resource = GetNode<ResourcePool>(_resourcePath);
            resource.Bind(nameof(resource.CurrentValue), this, nameof(ProgressBar.Value));
            resource.Bind(nameof(resource.Value), this, nameof(ProgressBar.MaxValue));
        }
    }
}
