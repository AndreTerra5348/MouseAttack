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
        protected override string PropertyName => nameof(_propertyOwner.CurrentValue);
        protected override float MaxValue => _propertyOwner.Value;
        protected override float StartingValue => MaxValue;
    }
}
