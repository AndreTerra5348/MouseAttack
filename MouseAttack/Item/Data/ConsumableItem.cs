using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class ConsumableItem : UsableItem
    {
        const string TypeName = "Consumable";
        public int MinBaseValue { get; private set; } = 1;
        public int MaxBaseValue { get; private set; } = 3;

        protected override Action ApplyAction => Use;

        int _count = 0;
        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public int DroppedCount { get; set; } = 0;

        public override string TooltipType => TypeName;
        public override string DropText => DroppedCount.ToString();
        public override bool IsDraggable => false;

        public override bool CanUse => ElapsedCooldown <= 0 && Count > 0;

        protected PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        protected int GetRandomCount(int monsterLevel) =>
            Random.Next(MinBaseValue * monsterLevel, MaxBaseValue * monsterLevel);

        public override void ItemDropped(int monsterLevel) =>
            Count += (DroppedCount = GetRandomCount(monsterLevel));

        public override void Use()
        {
            base.Use();
            Count--;
            if (Count <= 0)
                PlayerInventory.Remove(this);
        }
    }
}
