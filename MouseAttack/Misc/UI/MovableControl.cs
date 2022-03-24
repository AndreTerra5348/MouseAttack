using Godot;
using MouseAttack.Constants;
using MouseAttack.World;
using System;

namespace MouseAttack.Misc.UI
{
    public class MovableControl : Node
    {
        [Export]
        float _speed = 20;
        Vector2 _mousePressedOffset = Vector2.Zero;
        Vector2 _nextPosition;
        Control _parent;
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        public override void _Ready()
        {
            SetProcess(false);
            _parent = GetParent<Control>();
            _parent.Connect(Signals.GuiInput, this, nameof(GuiInput));
        }

        public override void _Process(float delta)
        {
            var newPosition = _parent.RectGlobalPosition.LinearInterpolate(_nextPosition, delta * _speed);
            _parent.RectGlobalPosition = newPosition;
        }
        public void GuiInput(InputEvent @event)
        {
            if (@event.IsActionPressed(InputAction.LMB))
            {
                _mousePressedOffset = _parent.RectGlobalPosition - (@event as InputEventMouseButton).GlobalPosition;
                CalculateNextPosition((@event as InputEventMouseButton).GlobalPosition);
                SetProcess(true);
                return;
            }
            else if (@event.IsActionReleased(InputAction.LMB))
                SetProcess(false);

            if (!(@event is InputEventMouseMotion))
                return;

            if (!IsProcessing())
                return;
            CalculateNextPosition((@event as InputEventMouseMotion).GlobalPosition);
        }

        private void CalculateNextPosition(Vector2 mousePosition)
        {
            _nextPosition = mousePosition + _mousePressedOffset;

            if (_nextPosition.x < PlayArea.RectGlobalPosition.x)
                _nextPosition.x = PlayArea.RectGlobalPosition.x;
            else if (_nextPosition.x + _parent.RectSize.x > PlayArea.RectGlobalPosition.x + PlayArea.RectSize.x)
                _nextPosition.x = PlayArea.RectGlobalPosition.x + PlayArea.RectSize.x - _parent.RectSize.x;

            if (_nextPosition.y < PlayArea.RectGlobalPosition.y)
                _nextPosition.y = PlayArea.RectGlobalPosition.y;
            else if (_nextPosition.y + _parent.RectSize.y > PlayArea.RectGlobalPosition.y + PlayArea.RectSize.y)
                _nextPosition.y = PlayArea.RectGlobalPosition.y + PlayArea.RectSize.y - _parent.RectSize.y;
        }
    }
}


