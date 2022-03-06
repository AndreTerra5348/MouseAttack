using Godot;
using System;
using MouseAttack.Constants;

namespace MouseAttack.Entity.UI
{
    public class FloatingText : Control
    {
        public string Text { get; set; } = "";
        public Color Color { get; set; } = Colors.White;

        public override void _Ready()
        {
            Label label = GetNode<Label>(nameof(Label));
            label.Text = Text;
            label.AddColorOverride(Overrides.FontColor, Color);
        }
    }
}
