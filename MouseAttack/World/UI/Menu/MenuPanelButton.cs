using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;

namespace MouseAttack.World.UI.Menu
{
    public class MenuPanelButton : SystemButton<SystemPanel>
    {
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;

        public override void _Ready()
        {
            base._Ready();
            Character.Listen(nameof(PlayerCharacter.Level), onChanged: () => SetAlertEnable(true));
            Panel.Listen(nameof(SystemPanel.Visible), onChanged: () => SetAlertEnable(false));
        }

    }
}
