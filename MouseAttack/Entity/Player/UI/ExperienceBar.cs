using Godot;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ExperienceBar : ProgressBarController<PlayerCharacter>
    {
        [Export]
        NodePath _levelLabelPath = "";
        Label _levelLabel;

        const string LevelTextFormat = "Level {0}";

        public override void _Ready()
        {
            base._Ready();
            _levelLabel = GetNode<Label>(_levelLabelPath);
            DataBindings.Add(new Binding(Source, nameof(Source.Experience), ProgressBar, nameof(ProgressBar.Value)));
            DataBindings.Add(new Binding(Source, nameof(Source.NextLevelExperience), ProgressBar, nameof(ProgressBar.MaxValue)));
            DataBindings.Add(new Binding(Source, nameof(Source.Level), _levelLabel, nameof(_levelLabel.Text), 
                (object value) => String.Format(LevelTextFormat, value)));

            Initialize();
        }
    }
}
