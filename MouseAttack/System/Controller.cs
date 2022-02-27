using Godot;
using System;

namespace MouseAttack.System
{

    public class Controller : Node2D
    {
        public class HotkeyPressedEventArgs : EventArgs
        {
            public readonly int Hotkey;
            public HotkeyPressedEventArgs(int hotkey) => Hotkey = hotkey;
        }

        public EventHandler LMBPressed;
        public EventHandler<HotkeyPressedEventArgs> HotkeyPressed;

        readonly string[] _hotkeys = { "hk1", "hk2", "hk3", "hk4", "hk5" };

        bool _lmbPressed = false;

        public override void _Process(float delta)
        {
            if (_lmbPressed)
                LMBPressed?.Invoke(this, new EventArgs());
        }

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            for (int i = 0; i < _hotkeys.Length; i++)
            {
                if (@event.IsActionPressed(_hotkeys[i]))
                    HotkeyPressed?.Invoke(this, new HotkeyPressedEventArgs(i));
            }
            base._UnhandledKeyInput(@event);
        }

        // Any release should disable mouse pressed
        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionReleased("LMB"))
                _lmbPressed = false;
            base._Input(@event);
        }

        // Only non-UI presses should enable mouse pressed
        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("LMB"))
                _lmbPressed = true;
            base._UnhandledInput(@event);
        }
    }
}

