using Godot;
using MouseAttack.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.Player.UI
{
    public class ActionBar : Control
    {
        [Export]
        NodePath _slotContainerPath = "";
        HBoxContainer _slotContainer;
        PlayerActor _playerActor;
        public override void _Ready()
        {
            _playerActor = GetParent<PlayerActor>();
            _playerActor.ActionSlotted += OnActionslotted;
            _playerActor.ActionUsed += OnActionUsed;
            _playerActor.PropertyChanged += OnPlayerActorPropertyChanged;

            _slotContainer = GetNode<HBoxContainer>(_slotContainerPath);
        }

        private void OnPlayerActorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(_playerActor.SelectedSlot))
                return;

            Button slotButton = _slotContainer.GetChild<Button>(_playerActor.SelectedSlot);
            slotButton.Pressed = true;
        }

        private void OnActionslotted(object sender, ActionSlottedEventArgs e)
        {
            Button slotButton = _slotContainer.GetChild<Button>(e.Slot);
            slotButton.Icon = e.Action.Icon;
        }

        private void OnActionUsed(object sender, ActionUsedEventArgs e)
        {
            Button slotButton = _slotContainer.GetChild<Button>(e.Slot);
            CooldownTextureProgress textureProgress = slotButton
                .GetNode<CooldownTextureProgress>(nameof(CooldownTextureProgress));

            textureProgress.StartCooldown(e.CooldownTimeout);
        }
    }
}
