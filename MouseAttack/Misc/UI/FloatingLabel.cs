using Godot;
using System;
using MouseAttack.Constants;
using MouseAttack.World;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabel : CanvasLayer
    {
        [Export]
        NodePath _controlPath = "";
        [Export]
        NodePath _containerPath = "";
        [Export]
        NodePath _labelPath = "";

        public string Text { get; set; } = "";
        public Vector2 Position { get; set; }
        public Control Icon { get; set; }
        public AnimationPlayer AnimationPlayer { get; private set; }
        public float AnimationPosition => AnimationPlayer.CurrentAnimationPosition;
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        public override void _Ready()
        {
            AnimationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
            Label label = GetNode<Label>(_labelPath);
            label.Text = Text;
            Control control = GetNode<Control>(_controlPath);
            HBoxContainer container = GetNode<HBoxContainer>(_containerPath);
            control.RectGlobalPosition = PlayArea.ClampPosition(Position, container.RectSize);
            if (Icon != null)
                control.AddChild(Icon);
            
        }
    }
}
