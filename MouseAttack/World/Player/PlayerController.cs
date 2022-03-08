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

        readonly string[] _hotkeys = { "hk1", "hk2", "hk3", "hk4", "hk5" };

        public override void _Process(float delta)
        {
            // Disable Process when opening menus
            // Re enable Process when closing menus
            if (Input.IsActionPressed("LMB"))
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

        public override void _Input(InputEvent @event)
        {
            // Open Inventory

            // Open Skill Menu
        }
    }
}

