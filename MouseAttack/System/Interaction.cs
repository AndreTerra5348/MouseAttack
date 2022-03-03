using Godot;
using MouseAttack.Action;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World;
using MouseAttack.Misc;
using MouseAttack.Entity.Player;

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
        Stage Stage => _stage ?? (_stage = this.GetStage());

        PlayerCharacter PlayerCharacter => Stage.PlayerEntity.Character;
        public override void _Ready()
        {
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

            if (!PlayerCharacter.HasEnoughMana(Action.Cost))
                return;

            PlayerCharacter.UseMana(Action.Cost);

            CommonEffect effectInstance = Action.GetEffectInstance<CommonEffect>();
            effectInstance.Action = Action;
            effectInstance.User = PlayerCharacter;
            effectInstance.Position = GetViewport().GetMousePosition();
            Stage.AddChild(effectInstance);
            Action.Use();

            float cooldownReduction = Action.CooldownTimeout * PlayerCharacter.CooldownReducion.Value;
            float cooldownTimeout = Action.CooldownTimeout - cooldownReduction;
            await ToSignal(GetTree().CreateTimer(cooldownTimeout), Signals.Timer.Timeout);

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
