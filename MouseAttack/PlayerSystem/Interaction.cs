using Godot;
using MouseAttack.Action;
using MouseAttack.Action.WorldEffect;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World;
using MouseAttack.Misc;
using MouseAttack.Entity.Player;
using MouseAttack.Constants;

namespace MouseAttack.PlayerSystem
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
            base._Ready();
            _stage = this.GetStage();
            var controller = GetNode<Controller>(nameof(Controller));
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;
            Action = SlottedActions.First();
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            // Use Action
            if (Action.OnCooldown)
                return;

            if (!_stage.PlayerEntity.Character.HasEnoughMana(Action.Cost))
                return;

            _stage.PlayerEntity.Character.UseMana(Action.Cost);

            CommonEffect effectInstance = Action.GetWorldEffectInstance<CommonEffect>();
            effectInstance.Action = Action;
            effectInstance.User = _stage.PlayerEntity.Character;
            effectInstance.Position = GetViewport().GetMousePosition();
            _stage.AddChild(effectInstance);
            Action.Use();

            float cooldownReduction = Action.CooldownTimeout * _stage.PlayerEntity.Character.CooldownReducion.Value;
            float cooldownTimeout = Action.CooldownTimeout - cooldownReduction;

            await ToSignal(GetTree().CreateTimer(cooldownTimeout), Signals.Timeout);

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
