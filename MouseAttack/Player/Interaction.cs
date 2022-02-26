using Godot;
using MouseAttack.Interaction;
using System.Collections.Generic;
using System.Linq;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using MouseAttack.World.Autoload;

namespace MouseAttack.Player
{
    public class Interaction : Node2D
    {
        [Export]
        public NodePath ControllerPath;
        [Export]
        public List<CommonActionData> commonActions;        

        CommonActionData _action;

        public override void _Ready()
        {
            var controller = GetNode<Controller>(ControllerPath);
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            _action = commonActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            if (_action.OnCooldown())
                return;

            var worldProxy = this.GetAutoload<WorldProxy>();
            var mousePosition = GetViewport().GetMousePosition();
            _action.Instantiate(worldProxy, mousePosition);
            _action.Use();

            await ToSignal(GetTree().CreateTimer(_action.CooldownTimeout), Signals.Timer.Timeout);

            _action.StopCooldown();
        }

        private void HotkeyPressed(object sender, Controller.HotkeyPressedEventArgs e)
        {
            var hotkey = e.Hotkey;
            if (hotkey >= commonActions.Count)
                return;

            _action = commonActions[hotkey];
        }
    }
}
