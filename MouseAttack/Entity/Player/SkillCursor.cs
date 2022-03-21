using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;

namespace MouseAttack.Entity.Player
{
    public class SkillCursor : Node2D
    {
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        public override void _Ready()
        {
            ZIndex = ZOrder.SkillCursor;
        }

        public override void _Draw()
        {
            if (!PlayArea.OnPlayArea)
                return;

            Vector2 topLeft = GetViewport().GetSnappedMousePosition(Values.CellSize);            
            Vector2 bottomLeft = topLeft + Vector2.Down * Values.CellSize.y;
            Vector2 bottomRight = bottomLeft + Vector2.Right * Values.CellSize.x;
            Vector2 topRight = topLeft + Vector2.Right * Values.CellSize.x;

            DrawLine(topLeft, bottomLeft, Colors.Red);
            DrawLine(bottomLeft, bottomRight, Colors.Red);
            DrawLine(bottomRight, topRight, Colors.Red);
            DrawLine(topRight, topLeft, Colors.Red);
        }

        public override void _Process(float delta) =>
            Update();
    }

}