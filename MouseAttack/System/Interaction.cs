using Godot;
using MouseAttack.Action;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World;
using MouseAttack.Misc;

namespace MouseAttack.System
{
    public class Interaction : ObservableNode
    {
        [Export]
        public CommonAction[] SlottedActions { get; private set; } = new CommonAction[5];

        CommonAction _action;
        public CommonAction Action
        {
            set
            {
                if (_action == value)
                    return;
                _action = value;
                OnPropertyChanged();
            }
            get => _action;
        }
        Stage _stage;


        public override void _Ready()
        {
            var controller = GetNode<Controller>(nameof(Controller));
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            _stage = this.GetStage();
            Action = SlottedActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            // Use Action
            if (Action.OnCooldown)
                return;

            if (!_stage.Player.HasEnoughMana(Action.Cost))
                return;

            _stage.Player.UseMana(Action.Cost);
            
            Action.Use(_stage);

            await ToSignal(GetTree().CreateTimer(Action.CooldownTimeout), Signals.Timer.Timeout);

            Action.StopCooldown();
        }

        private void HotkeyPressed(object sender, Controller.HotkeyPressedEventArgs e)
        {
            // Select Action
            var hotkey = e.Hotkey;
            if (hotkey >= SlottedActions.Length)
                return;

            Action = SlottedActions[hotkey];
        }

        public void SetAction(CommonAction action, int slot) => SlottedActions[slot] = action;
        public void RemoveAction(int slot) => SlottedActions[slot] = null;


    }
}
