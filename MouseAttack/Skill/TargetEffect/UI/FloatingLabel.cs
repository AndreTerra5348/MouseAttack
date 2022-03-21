using Godot;
using System;
using MouseAttack.Constants;

namespace MouseAttack.Skill.TargetEffect.UI
{
    public class FloatingLabel : CanvasLayer
    {
        [Export]
        NodePath _containerPath = "";
        [Export]
        NodePath _labelPath = "";

        public string Text { get; set; } = "";
        public Color Color { get; set; } = Colors.White;
        public Vector2 Position { get; set; }
        public override void _Ready()
        {
            Label label = GetNode<Label>(_labelPath);
            label.Text = Text;
            label.AddColorOverride(Overrides.FontColor, Color);
            Control container = GetNode<Control>(_containerPath);
            container.RectGlobalPosition = Position;
        }
    }
}
