using Godot;
using System;
using MouseAttack.Constants;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabel : CanvasLayer
    {
        [Export]
        NodePath _containerPath = "";
        [Export]
        NodePath _labelPath = "";

        public string Text { get; set; } = "";
        public Vector2 Position { get; set; }
        public AnimationPlayer AnimationPlayer { get; private set; }
        public float AnimationPosition => AnimationPlayer.CurrentAnimationPosition;

        public override void _Ready()
        {
            AnimationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
            Label label = GetNode<Label>(_labelPath);
            label.Text = Text;
            Control container = GetNode<Control>(_containerPath);
            container.RectGlobalPosition = Position;
        }
    }
}
