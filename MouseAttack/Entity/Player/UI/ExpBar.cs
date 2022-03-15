using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ExpBar : ProgressBar
    {
        [Export]
        NodePath _characterPath = "";
        public override void _Ready()
        {
            base._Ready();
            PlayerCharacter character = GetNode<PlayerCharacter>(_characterPath);
            character.Bind(nameof(character.Experience), this, nameof(ProgressBar.Value));
            character.Bind(nameof(character.NextLevelExperience), this, nameof(ProgressBar.MaxValue));
        }
    }
}
