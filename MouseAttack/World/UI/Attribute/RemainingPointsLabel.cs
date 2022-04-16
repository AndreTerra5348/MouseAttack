using Godot;
using MouseAttack.Constants;
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
        private bool _onAlert;

        [Export]
        float BlickTimeout { get; set; } = 0.5f;
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        Color _currentColor = Colors.White;

        public override void _Ready()
        {
            Character.Bind(nameof(PlayerCharacter.AttributePoints), this, nameof(Text),
                propertyConvertor: (value) => string.Format(RemainingPointsTextFormat, value));
            Character.Listen(nameof(PlayerCharacter.AttributePoints), 
                onChanged: () =>
                {
                    _onAlert = Character.AttributePoints > 0;
                    Visible = _onAlert;
                    BlinkAlertIcon();

                });
            Hide();
        }

        async void BlinkAlertIcon()
        {
            _currentColor = _currentColor == Colors.White ? Colors.Goldenrod : Colors.White;
            AddColorOverride(Overrides.FontColor, _currentColor);
            await this.CreateTimer(BlickTimeout);
            if (_onAlert)
                BlinkAlertIcon();
            else
                AddColorOverride(Overrides.FontColor, Colors.White);
        }
    }
}
