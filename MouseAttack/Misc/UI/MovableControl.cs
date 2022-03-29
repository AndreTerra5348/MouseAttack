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
            _parent.Connect(Signals.GuiInput, this, nameof(ParentGuiInput));
        }

        public override void _Process(float delta)
        {
            var newPosition = _parent.RectGlobalPosition.LinearInterpolate(_nextPosition, delta * _speed);
            _parent.RectGlobalPosition = newPosition;
        }

        public void ParentGuiInput(InputEvent @event)
        {
            if (@event.IsActionPressed(InputAction.LMB))
            {
                InputEventMouseButton buttonEvent = @event as InputEventMouseButton;
                _mousePressedOffset = _parent.RectGlobalPosition - buttonEvent.GlobalPosition;
                _nextPosition = CalculateNextPosition(buttonEvent.GlobalPosition);
                SetProcess(true);
                return;
            }
            else if (@event.IsActionReleased(InputAction.LMB))
                SetProcess(false);

            if (!IsProcessing())
                return;

            InputEventMouseMotion motionEvent = @event as InputEventMouseMotion;
            if (motionEvent == null)
                return;
            
            _nextPosition = CalculateNextPosition(motionEvent.GlobalPosition);
        }

        private Vector2 CalculateNextPosition(Vector2 mousePosition) =>
            PlayArea.ClampPosition(mousePosition + _mousePressedOffset, _parent.RectSize);
    }
}


