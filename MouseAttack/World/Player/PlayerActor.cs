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
using static MouseAttack.World.Player.PlayerController;

namespace MouseAttack.World.Player
{
    public class ActionSlottedEventArgs : EventArgs
    {
        public readonly CommonAction Action;
        public readonly int Slot;

        public ActionSlottedEventArgs(CommonAction action, int slot)
        {
            Action = action;
            Slot = slot;
        }
    }
    public class ActionUsedEventArgs : EventArgs
    {
        public readonly int Slot;
        public readonly float CooldownTimeout;

        public ActionUsedEventArgs(int slot, float cooldownTimeout)
        {
            Slot = slot;
            CooldownTimeout = cooldownTimeout;
        }
    }
    public class PlayerActor : ObservableNode
    {
        public event EventHandler<ActionSlottedEventArgs> ActionSlotted;
        public event EventHandler<ActionUsedEventArgs> ActionUsed;

        [Export]
        public ActionDB _actionDB;
        [Export]
        NodePath _controllerPath = "";

        CommonAction[] _slottedActions = new CommonAction[5];

        int _selectSlot = 0;
        public int SelectedSlot
        {
            set
            {
                if (_selectSlot == value)
                    return;
                _selectSlot = value;
                OnPropertyChanged();
            }
            get => _selectSlot;
        }

        bool IsSelectedSlotNull => _slottedActions[SelectedSlot] == null;
        PlayerCharacter PlayerCharacter => _stage.PlayerEntity.Character;

        Stage _stage;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
            var controller = GetNode<PlayerController>(_controllerPath);
            controller.LMBPressed += OnLMBPressed;
            controller.HotkeyPressed += HotkeyPressed;

            SlotAction(_actionDB.GetMainAttack(), _selectSlot);
        }

        async private void OnLMBPressed(object sender, EventArgs e)
        {
            if (IsSelectedSlotNull)
                return;

            CommonAction action = _slottedActions[SelectedSlot];

            if (action.OnCooldown)
                return;

            if (!PlayerCharacter.HasEnoughMana(action.Cost))
                return;

            PlayerCharacter.UseMana(action.Cost);

            CommonWorldEffect effectInstance = action.GetWorldEffectInstance<CommonWorldEffect>();
            effectInstance.Action = action;
            effectInstance.User = PlayerCharacter;
            effectInstance.Position = GetViewport().GetMousePosition();
            _stage.AddChild(effectInstance);
            action.Use();

            float cooldownTimeout = action.CalculateCooldownTimeout(PlayerCharacter.CooldownReducion);

            ActionUsed?.Invoke(this, new ActionUsedEventArgs(SelectedSlot, cooldownTimeout));

            await ToSignal(GetTree().CreateTimer(cooldownTimeout), Signals.Timeout);

            action.StopCooldown();
        }

        private void HotkeyPressed(object sender, HotkeyPressedEventArgs e)
        {
            var hotkey = e.Hotkey;
            if (hotkey >= _slottedActions.Length)
                return;

            SelectedSlot = hotkey;
        }

        public void SlotAction(CommonAction action, int slot)
        {
            _slottedActions[slot] = action;
            ActionSlotted?.Invoke(this, new ActionSlottedEventArgs(action, slot));
        }

        public void RemoveAction(int slot) => _slottedActions[slot] = null;


    }
}
