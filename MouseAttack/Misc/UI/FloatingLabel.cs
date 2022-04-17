using Godot;
using System;
using MouseAttack.Constants;
using MouseAttack.World;
using MouseAttack.Extensions;
using MouseAttack.Item;
using MouseAttack.Item.Data;

namespace MouseAttack.Misc.UI
{
    public class FloatingLabel : PanelContainer
    {
        [Export]
        NodePath ContainerPath { get; set; }
        [Export]
        NodePath LabelPath { get; set; }

        public string Text { get; set; } = "";
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public int FontSize { get; set; } = 13;

        public AnimationPlayer AnimationPlayer { get; private set; }
        public float AnimationPosition => AnimationPlayer.CurrentAnimationPosition;
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();

        async public override void _Ready()
        {
            Hide();
            AnimationPlayer = GetNode<AnimationPlayer>(nameof(AnimationPlayer));
            var label = GetNode<Label>(LabelPath);
            label.Text = Text;
            var font = label.GetFont("font") as DynamicFont;
            font.Size = FontSize;
            if (Color.a > 0.0f)
                label.AddColorOverride(Overrides.FontColor, Color);

            // Update Position
            await this.SkipNextFrame();
            var container = GetNode<CenterContainer>(ContainerPath);
            RectGlobalPosition = PlayArea.ClampPosition(Position, container.RectSize);
            Show();

            // Queue QueueFree
            await ToSignal(AnimationPlayer, Signals.AnimationFinished);
            QueueFree();
        }
    }
}
