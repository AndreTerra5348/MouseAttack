using Godot;
using MouseAttack.Action;
using System.Collections.Generic;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World.Autoload;

namespace MouseAttack.System
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
            // Use Action
            if (_action.OnCooldown())
                return;

            CommonAction effectScene = _action.GetEffectInstance();
            var worldProxy = this.GetAutoload<WorldProxy>();
            worldProxy.AddChildAtMousePosition(effectScene);
            effectScene.SetData(_action);
            _action.Use();

            await ToSignal(GetTree().CreateTimer(_action.CooldownTimeout), Signals.Timer.Timeout);

            _action.StopCooldown();
        }

        private void HotkeyPressed(object sender, Controller.HotkeyPressedEventArgs e)
        {
            // Select Action
            var hotkey = e.Hotkey;
            if (hotkey >= commonActions.Count)
                return;

            _action = commonActions[hotkey];
        }
    }
}
