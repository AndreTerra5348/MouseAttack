﻿using Godot;
using MouseAttack.Skill.Data;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Entity.Player.UI.Inventory;
using MouseAttack.Item.Data;
using MouseAttack.Misc;

namespace MouseAttack.Entity.Player.UI.Skill
{
    public class SkillSlot : Slot
    {
        [Export]
        NodePath _cooldownPath = "";
        CooldownProgressBar _cooldown;

        public override void _Ready()
        {
            base._Ready();
            _cooldown = GetNode<CooldownProgressBar>(_cooldownPath);
            RemoveItem();
        }

        public void Use(int cooldown) =>
            _cooldown.StartCooldown(cooldown);

        public override bool CanDropData(SlotDragData data) =>
            data?.Item is CommonSkill;

        protected override void ItemDragged()
        {
            Item.IsSlotted = false;
            RemoveItem();
        }

        protected override void ItemDropped(CommonItem item)
        {
            GetParent()
                .GetChildren()
                .Cast<SkillSlot>()
                .FirstOrDefault(x => x.Item == item)
                ?.RemoveItem();
        }

        protected override void OnRightClick()
        {
        }
    }
}