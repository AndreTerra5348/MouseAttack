using Godot;
using MouseAttack.Action;
using MouseAttack.Action.WorldEffect;
using System.Linq;
using MouseAttack.Extensions;
using System;
using MouseAttack.World;
using MouseAttack.Misc;
using MouseAttack.Constants;

namespace MouseAttack.Entity.Player
{
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

    public class PlayerActionController : ObservableNode
    {
        public event EventHandler<ActionUsedEventArgs> ActionUsed;

        [Export]
        public ActionDB ActionDB { get; private set; }

        readonly CommonAction[] _slottedActions = new CommonAction[5];
        readonly string[] _hotkeys = { "hk1", "hk2", "hk3", "hk4", "hk5" };
        Stage _stage;
        bool _lmbPressed = false;

        bool IsSelectedSlotEmpty => _slottedActions[SelectedSlotIndex] == null;
        PlayerCharacter PlayerCharacter => _stage.PlayerEntity.Character;

        int _selectSlot = 0;
        public int SelectedSlotIndex
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

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
        }        

        public override void _Notification(int what)
        {
            if (what == NotificationPaused)
                _lmbPressed = false;
        }

        async public override void _Process(float delta)
        {
            if (!_lmbPressed)
                return;

            if (IsSelectedSlotEmpty)
                return;

            CommonAction action = _slottedActions[SelectedSlotIndex];

            if (action.OnCooldown)
                return;

            if (!PlayerCharacter.HasEnoughMana(action.Cost))
                return;

            PlayerCharacter.UseMana(action.Cost);

            CommonWorldEffect worldEffect = action.GetWorldEffectInstance<CommonWorldEffect>();
            worldEffect.Action = action;
            worldEffect.User = PlayerCharacter;
            worldEffect.Position = GetViewport().GetMousePosition();
            _stage.AddChild(worldEffect);
            action.Use();

            float cooldownTimeout = action.CalculateCooldownTimeout(PlayerCharacter.CooldownReducion);

            ActionUsed?.Invoke(this, new ActionUsedEventArgs(SelectedSlotIndex, cooldownTimeout));

            await ToSignal(GetTree().CreateTimer(cooldownTimeout), Signals.Timeout);

            action.StopCooldown();
        }

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            for (int i = 0; i < _hotkeys.Length; i++)
            {
                if (!@event.IsActionPressed(_hotkeys[i]))
                    continue;
                SelectedSlotIndex = i;
                break;
            }

        }

        public override void _UnhandledInput(InputEvent @event)
        {
            // Pressed only when there is no UI
            if (@event.IsActionPressed("LMB"))
                _lmbPressed = true;
        }

        public override void _Input(InputEvent @event)
        {
            // Release every where
            if (@event.IsActionReleased("LMB"))
                _lmbPressed = false;
        }

        public void SetAction(CommonAction action, int slot) =>
            _slottedActions[slot] = action;

    }
}