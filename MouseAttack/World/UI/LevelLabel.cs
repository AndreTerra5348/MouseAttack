using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;

namespace MouseAttack.World.UI
{
    public class LevelLabel : Label
    {
        const string LevelTextFormat = "Lv {0}";
        PlayerEntity PlayerEntity => 
            TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => 
            PlayerEntity.Character;

        FloatingLabelProvider FloatingLabelProvider =>
            TreeSharer.GetNode<FloatingLabelProvider>();
        FloatingLabelLayer FloatingLabelLayer =>
            TreeSharer.GetNode<FloatingLabelLayer>();
        public override void _Ready()
        {
            Character.Listen(nameof(PlayerCharacter.Level), onChanged: OnLevelUp);
            Character.Bind(nameof(PlayerCharacter.Level), this, nameof(Text),
                    propertyConvertor: (value) => string.Format(LevelTextFormat, value));
        }

        private void OnLevelUp()
        {
            Text = string.Format(LevelTextFormat, Character.Level);
            FloatingLabel label = FloatingLabelProvider.GetLabel();
            label.Text = "Level Up";
            label.Position = PlayerEntity.GlobalPosition;
            label.FontSize = 21;
            label.Color = Colors.Goldenrod;
            FloatingLabelLayer.AddChild(label);

        }
    }
}
