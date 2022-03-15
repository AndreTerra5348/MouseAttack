using Godot;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;


namespace MouseAttack.Entity.Player.UI
{
    public class LevelLabel : Label
    {
        const string LevelTextFormat = "Lv {0}";
        [Export]
        NodePath _characterPath = "";

        public override void _Ready()
        {
            base._Ready();
            PlayerCharacter character = GetNode<PlayerCharacter>(_characterPath);
            character.Bind(nameof(character.Level), this, nameof(Label.Text),
                propertyConvertor: (object value) => String.Format(LevelTextFormat, value));
        }
    }
}
