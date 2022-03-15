using Godot;
using MouseAttack.Action;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class ActionBar : Control
    {
        [Export]
        NodePath _playerActionControllerPath = "";
        PlayerActionController _playerActionController;

        int SelectedSlotIndex
        {
            get => _playerActionController.SelectedSlotIndex;
            set => _playerActionController.SelectedSlotIndex = value;
        }

        public override void _Ready()
        {
            _playerActionController = GetNode<PlayerActionController>(_playerActionControllerPath);
            _playerActionController.ActionUsed += OnActionUsed;

            _playerActionController.Listen(nameof(PlayerActionController.SelectedSlotIndex), 
                onChanged: OnPlayerActionControllerSlotSelected);

            foreach(var child in GetChildren())
            {
                ActionSlot slot = child as ActionSlot;
                int index = slot.GetIndex();

                slot.Listen(nameof(ActionSlot.Action),
                    onChanged: () => OnActionSlotChanged(slot, index));

                slot.Connect(Signals.Pressed, this, nameof(OnActionSlotSelected),
                    new Godot.Collections.Array { index });
            }

            ActionSlot slotZero = GetChild<ActionSlot>(0);
            var mainAttack = _playerActionController.ActionDB.GetMainAttack();
            slotZero.SetAction(mainAttack);
        }

        private void OnActionSlotSelected(int selectedSlotIndex) =>
            SelectedSlotIndex = selectedSlotIndex;

        private void OnActionSlotChanged(ActionSlot slot, int index) =>
            _playerActionController.SetAction(slot.Action, index);

        private void OnPlayerActionControllerSlotSelected() =>
            GetSelectedSlot().Pressed = true;

        private void OnActionUsed(object sender, ActionUsedEventArgs e) =>
            GetChild<ActionSlot>(e.Slot).Use(e.CooldownTimeout);

        private ActionSlot GetSelectedSlot() => 
            GetChild<ActionSlot>(SelectedSlotIndex);
    }
}
