using Godot;
using MouseAttack.Misc;
using MouseAttack.World.UI.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Shop
{
    public class ShopPanel : PanelContainer
    {
        [Export]
        NodePath CooldownPath { get; set; }
        [Export]
        NodePath SlotContainerPath { get; set; }
        [Export]
        int Cooldown { get; set; } = 5;
        int _elapsedCooldown = 0;

        CooldownProgressBar _cooldownBar;
        List<ShopSlot> _slots;
        Control _slotContainer;
        GridController GridController => TreeSharer.GetNode<GridController>();


        public override void _Ready()
        {
            _cooldownBar = GetNode<CooldownProgressBar>(CooldownPath);
            _slotContainer = GetNode<Control>(SlotContainerPath);
            _slots = _slotContainer.GetChildren().OfType<ShopSlot>().ToList();

            foreach(var slot in _slots)
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
            _slotContainer.Hide();
            _cooldownBar.Show();
            _cooldownBar.Start(_elapsedCooldown = Cooldown);
            GridController.RoundFinished += OnRoundFinished;
        }

        private void OnRoundFinished(object sender, EventArgs e)
        {
            _elapsedCooldown--;
            if (_elapsedCooldown > 0)
                return;

            GridController.RoundFinished -= OnRoundFinished;
            foreach (var slot in _slots)
            {
                slot.UpdateItem();
            }
            _slotContainer.Show();
            _cooldownBar.Hide();
        }
    }
}
