using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.GUI
{
    public class CooldownBar : TextureProgress
    {
        [Export]
        bool LabelDisabled { get; set; } = false;
        [Export]
        string Prefix { get; set; } = "";
        [Export]
        string Sufix { get; set; } = "";
        Label _label;
        GridController GridController => TreeSharer.GetNode<GridController>();

        public override void _Ready()
        {
            _label = GetNode<Label>(nameof(Label));
            _label.Visible = !LabelDisabled;
        }

        public override void _EnterTree() =>
            GridController.RoundFinished += OnRoundFinished;

        public override void _ExitTree() =>
            GridController.RoundFinished -= OnRoundFinished;

        private void OnRoundFinished(object sender, EventArgs e)
        {
            if (!Visible)
                return;
            Value--;
            UpdateLabel();
            if (Value == 0)
                Hide();
        }

        public void Start(int cooldown)
        {
            MaxValue = cooldown;
            Value = cooldown;
            UpdateLabel();
            Show();
        }

        void UpdateLabel() =>
            _label.Text = $"{Prefix} {Value.ToString("0")} {Sufix}";
    }
}
