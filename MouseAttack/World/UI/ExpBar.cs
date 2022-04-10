using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI
{
    public class ExpBar : ProgressBar
    {
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        public override void _Ready()
        {
            Character.Bind(nameof(PlayerCharacter.Experience), this, nameof(Value));
            Character.Bind(nameof(PlayerCharacter.NextLevelExperience), this, nameof(MaxValue));
        }
    }
}
