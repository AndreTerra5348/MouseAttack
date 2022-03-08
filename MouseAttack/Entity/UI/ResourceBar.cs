using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class ResourceBar : ObserverProgressBar<ResourcePool>
    {
        protected override List<PropertyBinding> Bindings => new List<PropertyBinding>()
        {
            new PropertyBinding(nameof(ProgressBar.Value), nameof(Source.CurrentValue)),
            new PropertyBinding(nameof(ProgressBar.MaxValue), nameof(Source.Value)),
        };
    }
}
