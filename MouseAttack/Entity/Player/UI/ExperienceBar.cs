using Godot;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ExperienceBar : ObserverProgressBar<PlayerCharacter>
    {
        protected override List<PropertyBinding> Bindings => new List<PropertyBinding>()
        {
            new PropertyBinding(nameof(ProgressBar.Value), nameof(Source.Experience)),
            new PropertyBinding(nameof(ProgressBar.MaxValue), nameof(Source.NextLevelExperience)),
        };
    }
}
