using Godot;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player
{
    public class ActionCursor : Node2D
    {
        public float Radius { get; set; } = 5.0f;
        public override void _Ready()
        {
            Input.SetMouseMode(Input.MouseMode.Hidden);
            ZIndex = ZOrder.ActionCursor;
        }

        public override void _Draw()
        {
            if (GetTree().Paused)
                return;
            DrawArc(GetViewport().GetMousePosition(), Radius, 0, Godot.Mathf.Tau, 10, Colors.Aqua);

        }

        public override void _Process(float delta)
        {

            Update();
        }

        public override void _Notification(int what)
        {
            if (what == NotificationPaused)
                Input.SetMouseMode(Input.MouseMode.Visible);
            else if (what == NotificationUnpaused)
                Input.SetMouseMode(Input.MouseMode.Hidden);
        }
    }

}