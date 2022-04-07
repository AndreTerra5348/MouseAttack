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
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        public override void _Ready()
        {
            PlayerEntity.Initialized += (s, e) =>
            {
                Character.Bind(nameof(PlayerCharacter.Experience), this, nameof(ProgressBar.Value));
                Character.Bind(nameof(PlayerCharacter.NextLevelExperience), this, nameof(ProgressBar.MaxValue));
            };
        }
    }
}
