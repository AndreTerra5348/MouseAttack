using Godot;
using MouseAttack.GUI;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Shop
{
    public class ShopPanel : PanelContainer
    {
        const string CooldownTextFormat = "Open in {0} Turn(s)";
        [Export]
        NodePath CooldownLabelPath { get; set; }
        [Export]
        NodePath SlotContainerPath { get; set; }
        [Export]
        int Cooldown { get; set; } = 5;
        int _elapsedCooldown = 0;

        Label _cooldownLabel;
        List<ShopSlot> _slots;
        Control _slotContainer;
        GridController GridController => TreeSharer.GetNode<GridController>();


        public override void _Ready()
        {
            _cooldownLabel = GetNode<Label>(CooldownLabelPath);
            _slotContainer = GetNode<Control>(SlotContainerPath);
            _slots = _slotContainer.GetChildren().OfType<ShopSlot>().ToList();

            foreach (var slot in _slots)
            {
                slot.ItemBought += (s, e) =>
                    Reset();
            }

            Reset();
        }

        void Reset()
        {
            foreach (var slot in _slots)
            {
                slot.Reset();
            }
            _elapsedCooldown = Cooldown;
            UpdateCooldownLabel();
            UpdateSlotContainer();
            GridController.RoundFinished += OnRoundFinished;
        }

        private void OnRoundFinished(object sender, EventArgs e)
        {
            _elapsedCooldown--;
            UpdateCooldownLabel();
            if (_elapsedCooldown > 0)
                return;

            GridController.RoundFinished -= OnRoundFinished;
            foreach (var slot in _slots)
            {
                slot.UpdateItem();
            }
            UpdateSlotContainer();
        }

        private void UpdateCooldownLabel()
        {
            _cooldownLabel.Text = String.Format(CooldownTextFormat, _elapsedCooldown);
            _cooldownLabel.Visible = _elapsedCooldown > 0;
        }

        private void UpdateSlotContainer() =>
            _slotContainer.Visible = _elapsedCooldown == 0;

    }
}
