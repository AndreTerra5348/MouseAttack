using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;

namespace MouseAttack.World
{
    public interface ICursorHoverable
    {
        void OnCursorEntered();
        void OnCursorExited();
    }
    public class PlayerCursor : Area2D
    {
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();

        public override void _EnterTree()
        {
            Connect(Signals.AreaEntered, this, nameof(OnAreaEntered));
            Connect(Signals.AreaExited, this, nameof(OnAreaExited));
        }

        public override void _Ready()
        {
            ZIndex = ZOrder.SkillCursor;
        }
        
        private void OnAreaEntered(Area2D area)
        {
            ICursorHoverable cursorHoverable = area as ICursorHoverable;
            if (cursorHoverable == null)
                return;
            cursorHoverable.OnCursorEntered();
        }
        private void OnAreaExited(Area2D area)
        {
            ICursorHoverable cursorHoverable = area as ICursorHoverable;
            if (cursorHoverable == null)
                return;
            cursorHoverable.OnCursorExited();
        }

        public override void _Draw()
        {
            if (!PlayArea.OnPlayArea)
                return;

            Vector2 topLeft = Vector2.Zero;
            Vector2 bottomLeft = topLeft + Vector2.Down * Values.CellSize.y;
            Vector2 bottomRight = bottomLeft + Vector2.Right * Values.CellSize.x;
            Vector2 topRight = topLeft + Vector2.Right * Values.CellSize.x;

            DrawLine(topLeft, bottomLeft, Colors.Red);
            DrawLine(bottomLeft, bottomRight, Colors.Red);
            DrawLine(bottomRight, topRight, Colors.Red);
            DrawLine(topRight, topLeft, Colors.Red);
        }

        public override void _Process(float delta)
        {
            Update();
            Position = GetViewport().GetSnappedMousePosition(Values.CellSize);

        }
    }

}