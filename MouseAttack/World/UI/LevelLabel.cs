using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;

namespace MouseAttack.World.UI
{
    public class LevelLabel : Label
    {
        const string LevelTextFormat = "Lv {0}";
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;

        public override void _Ready()
        {
            Character.Bind(nameof(PlayerCharacter.Level), this, nameof(Text),
                    propertyConvertor: (value) => string.Format(LevelTextFormat, value));
        }
    }
}
