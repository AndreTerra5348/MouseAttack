using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity.Player.UI;
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
    public class ResourceBar : ProgressBarController<ResourcePool>
    {
        public override void _Ready()
        {
            base._Ready();
            Source.Bind(nameof(Source.CurrentValue), ProgressBar, nameof(ProgressBar.Value));
            Source.Bind(nameof(Source.Value), ProgressBar, nameof(ProgressBar.MaxValue));
        }
    }
}
