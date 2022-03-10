using Godot;
using System;

namespace MouseAttack.World.Player
{
    public class PlayerController : Node
    {
        public class HotkeyPressedEventArgs : EventArgs
        {
            public readonly int Hotkey;
            public HotkeyPressedEventArgs(int hotkey) => Hotkey = hotkey;
        }

        public EventHandler LMBPressed;
        public EventHandler<HotkeyPressedEventArgs> HotkeyPressed;

        private bool _lmbPressed = false;
        readonly string[] _hotkeys = { "hk1", "hk2", "hk3", "hk4", "hk5" };

        public override void _Notification(int what)
        {
            if(what == NotificationPaused)
                _lmbPressed = false;
        }

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
            
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            // Pressed only when there is no UI
            if (@event.IsActionPressed("LMB"))
                _lmbPressed = true;
        }

        public override void _Input(InputEvent @event)
        {
            // Release every where
            if (@event.IsActionReleased("LMB"))
                _lmbPressed = false;

            // Open Inventory

            // Open Skill Menu
        }
    }
}

