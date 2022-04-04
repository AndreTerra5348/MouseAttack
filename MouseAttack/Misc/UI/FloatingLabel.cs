using Godot;
using System;
using MouseAttack.Constants;
using MouseAttack.World;
using MouseAttack.Extensions;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabel : PanelContainer
    {
        [Export]
        NodePath _containerPath = "";
        [Export]
        NodePath _labelPath = "";

        public string Text { get; set; } = "";
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Control Icon { get; set; }
        public AnimationPlayer AnimationPlayer { get; private set; }
        public float AnimationPosition => AnimationPlayer.CurrentAnimationPosition;
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        async public override void _Ready()
        {
            Hide();
            AnimationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
            Label label = GetNode<Label>(_labelPath);
            label.Text = Text;
            HBoxContainer container = GetNode<HBoxContainer>(_containerPath);            

            if (Icon != null)
                container.AddChild(Icon);
            if (Color.a != 0.0f)
                label.AddColorOverride(Overrides.FontColor, Color);            

            await this.SkipNextFrame();
            RectGlobalPosition = PlayArea.ClampPosition(Position, container.RectSize);
            Show();

            await ToSignal(AnimationPlayer, Signals.AnimationFinished);
            QueueFree();
        }
    }
}
