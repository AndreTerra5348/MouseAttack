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
        const string RestockTextFormt = "Restock in {0} Turn(s)";
        [Export]
        NodePath CooldownLabelPath { get; set; }
        [Export]
        NodePath SlotContainerPath { get; set; }
        [Export]
        int Cooldown { get; set; } = 5;
        int _elapsedCooldown = 0;
        bool _onSale = false;
        Label _cooldownLabel;
        List<ShopSlot> _slots;
        Control _slotContainer;
        GridController GridController => TreeSharer.GetNode<GridController>();


        public override void _Ready()
        {
            _cooldownLabel = GetNode<Label>(CooldownLabelPath);
            _slotContainer = GetNode<Control>(SlotContainerPath);
            _slots = _slotContainer.GetChildren().OfType<ShopSlot>().ToList();

            GridController.RoundFinished += OnRoundFinished;

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
            _onSale = false;
            UpdateCooldownLabel();
            UpdateSlotContainer();
            
        }

        private void OnRoundFinished(object sender, EventArgs e)
        {
            _elapsedCooldown--;
            UpdateCooldownLabel();

            if (_elapsedCooldown > 0)
                return;

            _onSale = true;
            _elapsedCooldown = Cooldown;

            foreach (var slot in _slots)
            {
                slot.UpdateItem();
            }
            UpdateSlotContainer();
            UpdateCooldownLabel();
        }

        private void UpdateCooldownLabel()
        {
            var format = _onSale ? RestockTextFormt : CooldownTextFormat;
            _cooldownLabel.Text = String.Format(format, _elapsedCooldown);
        }

        private void UpdateSlotContainer() =>
            _slotContainer.Visible = _onSale;

    }
}
