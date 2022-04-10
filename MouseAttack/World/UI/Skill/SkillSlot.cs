using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Constants;
using MouseAttack.Entity.Player.Inventory;

namespace MouseAttack.World.UI.Skill
{
    public class SkillSlot : Slot
    {
        // Reset and Update Cooldown when Item dragged or dropped
        [Export]
        NodePath CooldownProgressPath = "";
        CooldownProgressBar _cooldownProgress;
        [Export]
        bool IsMainAttack { get; set; } = false;

        GridController GridController => TreeSharer.GetNode<GridController>();
        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();
        public override void _Ready()
        {
            base._Ready();
            _cooldownProgress = GetNode<CooldownProgressBar>(CooldownProgressPath);
            UnsetItem();

            if (!IsMainAttack)
                return;

            Item = PlayerInventory.MainAttack;
            Item.IsSlotted = true;
        }

        public override void _Input(InputEvent @event)
        {
            if (GridController.IsTurnElapsing)
                return;

            if (@event.IsActionPressed(String.Format(InputAction.HotkeyFormat, GetIndex() + 1)))
                Pressed = true;

            if (!Pressed || Item == null)
                return;

            if (!@event.IsActionPressed(InputAction.LMB))
                return;            

            var skill = Item as CommonSkill;
            if (!skill.CanUse)
                return;

            skill.Use();
            _cooldownProgress.Start(skill.Cooldown);

            GridController.ElapseTurn();
        }


        public void Use(int cooldown) =>
            _cooldownProgress.Start(cooldown);

        public override bool CanDropData(CommonItem data) =>
            data is CommonSkill;

        protected override void ItemDragged()
        {
            UnslotItem();
            UnsetItem();
        }

        protected override void OnRightClick()
        {
        }
    }
}