using Godot;
using MouseAttack.Action;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.Player;
using MouseAttack.World;

namespace MouseAttack.System
{
    public class Interaction : Node
    {
        [Export]
        NodePath _controllerPath = null;
        [Export]
        CommonAction[] _slottedActions = new CommonAction[5];

        CommonAction _action;
        Stage _stage;


        public override void _Ready()
        {
            var controller = GetNode<Controller>(_controllerPath);
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            _stage = this.GetStage();
            _action = _slottedActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            // Use Action
            if (_action.OnCooldown)
                return;

            if (!_stage.Player.HasEnoughMana(_action.Cost))
                return;

            _stage.Player.UseMana(_action.Cost);
            
            _action.Use(_stage);

            await ToSignal(GetTree().CreateTimer(_action.CooldownTimeout), Signals.Timer.Timeout);

            _action.StopCooldown();
        }

        private void HotkeyPressed(object sender, Controller.HotkeyPressedEventArgs e)
        {
            // Select Action
            var hotkey = e.Hotkey;
            if (hotkey >= _slottedActions.Length)
                return;

            _action = _slottedActions[hotkey];
        }

        public void SetAction(CommonAction action, int slot) => _slottedActions[slot] = action;
        public void RemoveAction(int slot) => _slottedActions[slot] = null;


    }
}
