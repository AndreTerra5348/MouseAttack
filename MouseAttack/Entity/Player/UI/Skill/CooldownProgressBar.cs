using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class CooldownProgressBar : ProgressBar
    {
        Label _label;

        public override void _Ready()
        {
            _label = GetNode<Label>(nameof(Label));
            TreeSharer.GetNode<GridController>().RoundFinished += (s, e) =>
            {
                Value--;
                _label.Text = Value.ToString("0");
                if (Value == 0)
                    Hide();
            };
        }
        public void StartCooldown(int cooldown)
        {
            MaxValue = cooldown;
            Value = cooldown;
            _label.Text = cooldown.ToString();
            Show();
        }
    }
}
