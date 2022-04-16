using Godot;
using System;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Constants;
using MouseAttack.Entity.Player.Inventory;
using System.ComponentModel;
using MouseAttack.Skill.Data;
using System.Linq;
using MouseAttack.GUI;

namespace MouseAttack.World.UI
{
    public class UsableSlot : Slot<UsableItem>
    {
        // Reset and Update Cooldown when Item dragged or dropped
        [Export]
        NodePath CooldownBarPath = "";
        CooldownBar _cooldownBar;
        [Export]
        bool IsMainAttack { get; set; } = false;

        GridController GridController =>
            TreeSharer.GetNode<GridController>();
        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public override void _Ready()
        {
            base._Ready();
            _cooldownBar = GetNode<CooldownBar>(CooldownBarPath);
            UnsetItem();

            if (!IsMainAttack)
                return;

            Item = PlayerInventory.MainAttack;
            Item.IsSlotted = true;

            PlayerInventory.Removed += (s, e) =>
            {
                if (e.Item != Item)
                    return;
                if (Item is ConsumableItem)
                    return;

                UnsetItem();
            };
        }

        public override void _Pressed()
        {
            if (ToggleMode)
                return;

            Use();
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed(string.Format(InputAction.HotkeyFormat, GetIndex() + 1)))
            {
                Pressed = true;
                _Pressed();
            }

            if (!Pressed)
                return;

            if (!@event.IsActionPressed(InputAction.LMB))
                return;

            Use();
        }

        private void Use()
        {
            if (GridController.IsTurnElapsing)
                return;

            if (Item == null)
                return;

            if (!Item.CanUse)
                return;

            Item.Use();
        }

        protected override void RegistryItemEvents(UsableItem item)
        {
            item.CooldownStarted += OnCooldownStarted;
            base.RegistryItemEvents(item);
        }


        protected override void UnregistryItemEvents(UsableItem item)
        {
            item.CooldownStarted -= OnCooldownStarted;
            base.UnregistryItemEvents(item);
        }

        private void OnCooldownStarted(object sender, CooldownEventArgs e) =>
            _cooldownBar.Start(e.Cooldown);


        protected override void ItemDragged()
        {
            UnslotItem();
            UnsetItem();
        }

        protected override void ItemDropped(UsableItem item)
        {
            base.ItemDropped(item);
            ToggleMode = item is CollidableSkill;
        }

        protected override void OnRightClick()
        {
        }
    }
}