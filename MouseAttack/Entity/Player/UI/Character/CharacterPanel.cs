using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Character
{
    public class CharacterPanel : PanelContainer
    {
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;        

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.IsActionPressed(InputAction.CharacterPanel))
            {
                Visible = !Visible;
                if (Visible)
                    Update();
            }
        }
    }
}
