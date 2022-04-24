using Godot;
using MouseAttack.GUI;
using MouseAttack.Misc;
using System;

namespace MouseAttack.World.UI.Buff
{
    public class BuffLabelPanel : PanelContainer
    {
        [Export]
        NodePath CooldownPath { get; set; }
        [Export]
        NodePath LabelPath { get; set; }
        public int Duration { get; set; }
        public string Text { get; set; }

        int _elapsedDuration = 0;

        GridController GridController =>
            TreeSharer.GetNode<GridController>();

        public override void _Ready()
        {
            var label = GetNode<Label>(LabelPath);
            label.Text = Text;
            var cooldownBar = GetNode<CooldownBar>(CooldownPath);
            cooldownBar.Start(Duration);
            _elapsedDuration = Duration;
        }

        public override void _EnterTree() =>
            GridController.RoundFinished += OnRoundFinished;

        public override void _ExitTree() =>
            GridController.RoundFinished -= OnRoundFinished;

        private void OnRoundFinished(object sender, EventArgs e)
        {
            _elapsedDuration--;
            if (_elapsedDuration > 0)
                return;
            QueueFree();
        }

    }
}
