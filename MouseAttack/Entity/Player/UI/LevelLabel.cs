using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;


namespace MouseAttack.Entity.Player.UI
{
    public class LevelLabel : Label
    {
        const string LevelTextFormat = "Lv {0}";
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;

        public override void _Ready()
        {
            PlayerEntity.Initialized += (s, e) =>
            {
                Character.Bind(nameof(PlayerCharacter.Level), this, nameof(Label.Text),
                    propertyConvertor: (object value) => String.Format(LevelTextFormat, value));
            };
        }
    }
}
