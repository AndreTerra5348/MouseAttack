using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Skill
{
    public class CooldownProgressBar : ProgressBar
    {
        Label _label;
        GridController GridController => TreeSharer.GetNode<GridController>();

        public override void _Ready()
        {
            _label = GetNode<Label>(nameof(Label));
            GridController.RoundFinished += OnRoundFinished;
        }
        public override void _ExitTree() =>
            GridController.RoundFinished -= OnRoundFinished;

        private void OnRoundFinished(object sender, EventArgs e)
        {
            Value--;
            _label.Text = Value.ToString("0");
            if (Value == 0)
                Hide();
        }        

        public void Start(int cooldown)
        {
            MaxValue = cooldown;
            Value = cooldown;
            _label.Text = cooldown.ToString();
            Show();
        }
    }
}
