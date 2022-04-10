using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public class ConsumableItem : CommonItem
    {
        public int MinBaseValue { get; private set; } = 1;
        public int MaxBaseValue { get; private set; } = 3;        

        protected PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

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
            }
        }

        int _value = 0;
        public override int Value 
        { 
            get => _value * Count; 
            set => _value = value; 
        }

        public override string TooltipType => Name;
        public override string DropText => Count.ToString();
        public override bool IsDraggable => false;

        protected int GetRandomValue(int monsterLevel) =>
            Random.Next(MinBaseValue * monsterLevel, MaxBaseValue * monsterLevel);

        public override void ItemDropped(int monsterLevel) =>
            Count = GetRandomValue(monsterLevel);

        public void Use()
        {
            Count -= 1;
            if (Count == 0)
                PlayerInventory.Remove(this);
        }
    }
}
