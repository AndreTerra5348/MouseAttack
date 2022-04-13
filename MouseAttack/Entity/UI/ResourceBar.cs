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
        const string LabelFormat = "{0}\\{1}";
        [Export]
        NodePath ResourcePath { get; set; } = "";
        [Export]
        NodePath LabelPath { get; set; } = "";
        public override void _Ready()
        {
            var resource = GetNode<ResourcePool>(ResourcePath);
            resource.Bind(nameof(resource.CurrentValue), this, nameof(ProgressBar.Value));
            resource.Bind(nameof(resource.MaxValue), this, nameof(ProgressBar.MaxValue));

            if (LabelPath.IsEmpty())
                return;
            var label = GetNode<Label>(LabelPath);
            resource.Bind(nameof(resource.CurrentValue), label, nameof(Label.Text), setImmediate: true,
                propertyConvertor: (value) => 
                    String.Format(LabelFormat, ((float)value).ToString("0.0"), resource.MaxValue.ToString("0.0")));
        }
    }
}
