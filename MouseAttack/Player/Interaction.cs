using Godot;
using MouseAttack.Interaction;
using System.Collections.Generic;
using System.Linq;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;

namespace MouseAttack.Player
{
    public class Interaction : Node2D
    {
        [Export]
        public NodePath ControllerPath;
        [Export]
        public List<CommonActionData> commonActions;        

        CommonActionData selected;

        public override void _Ready()
        {
            var controller = GetNode<Controller>(ControllerPath);
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            selected = commonActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            if (selected.OnCooldown())
                return;

            var worldProxy = this.GetAutoload<WorldProxy>();
            var mousePosition = GetViewport().GetMousePosition();
            selected.Instantiate(worldProxy, mousePosition);
            selected.Use();

            await ToSignal(GetTree().CreateTimer(selected.CooldownTimeout), Signals.Timer.Timeout);

            selected.StopCooldown();
        }

        private void HotkeyPressed(object sender, Controller.HotkeyPressedEventArgs e)
        {
            var hotkey = e.Hotkey;
            if (hotkey >= commonActions.Count)
                return;

            selected = commonActions[hotkey];
        }
    }
}
