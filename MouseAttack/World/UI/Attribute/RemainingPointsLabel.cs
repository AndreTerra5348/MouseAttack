using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Attributes
{
    public class RemainingPointsLabel : Label
    {
        const string RemainingPointsTextFormat = "Remaining Points: {0}";
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        public override void _Ready()
        {
            Character.Bind(nameof(PlayerCharacter.AttributePoints), this, nameof(Text),
                propertyConvertor: (value) => string.Format(RemainingPointsTextFormat, value));
            Character.Listen(nameof(PlayerCharacter.AttributePoints), onChanged: () => Visible = Character.AttributePoints > 0);
            Hide();
        }
    }
}
