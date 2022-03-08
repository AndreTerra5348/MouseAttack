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
        protected override string PropertyName => nameof(_propertyOwner.Experience);
        protected override float MaxValue => 100.0f;
        protected override float StartingValue => 0.0f;
    }
}
