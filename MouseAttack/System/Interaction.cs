using Godot;
using MouseAttack.Action;
using System.Collections.Generic;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World.Autoload;
using MouseAttack.Entity;
using MouseAttack.Player;

namespace MouseAttack.System
{
    public class Interaction : Node2D
    {
        [Export]
        NodePath _controllerPath = null;
        [Export]
        NodePath _playerPath = null;
        [Export]
        CommonActionData[] _slottedActions = new CommonActionData[5];

        CommonActionData _action;
        PlayerEntity _player;
        public void SetAction(CommonActionData action, int slot) => _slottedActions[slot] = action;

        public void RemoveAction(int slot) => _slottedActions[slot] = null;

        public override void _Ready()
        {
            var controller = GetNode<Controller>(_controllerPath);
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            _player = GetNode<PlayerEntity>(_playerPath);

            _action = _slottedActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            // Use Action
            if (_action.OnCooldown())
                return;

            if (!_player.HasEnoughMana(_action.Cost))
                return;

            _player.UseMana(_action.Cost);
            
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
            if (hotkey >= _slottedActions.Length)
                return;

            _action = _slottedActions[hotkey];
        }


    }
}
