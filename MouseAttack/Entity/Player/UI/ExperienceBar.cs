using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ExperienceBar : ProgressBarController<PlayerCharacter>
    {
        const string LevelTextFormat = "Level {0}";
        [Export]
        NodePath _levelLabelPath = "";

        public override void _Ready()
        {
            base._Ready();
            Label levelLabel = GetNode<Label>(_levelLabelPath);
            Source.Bind(nameof(Source.Experience), ProgressBar, nameof(ProgressBar.Value));
            Source.Bind(nameof(Source.NextLevelExperience), ProgressBar, nameof(ProgressBar.MaxValue));
            Source.Bind(nameof(Source.Level), levelLabel, nameof(Label.Text), 
                propertyConvertor : (object value) => String.Format(LevelTextFormat, value));
        }
    }
}
