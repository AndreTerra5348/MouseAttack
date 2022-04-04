using Godot;
using MouseAttack.Entity.Player;
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
            protected set => _value = value; 
        }

        public override string TooltipType => Name;
        public override string DropText => String.Empty;
        public override bool IsDraggable => false;
        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void ItemDropped(int monsterLevel)
        {
            Count += Random.Next(MinBaseValue * monsterLevel, MaxBaseValue * monsterLevel);
            var item = PlayerInventory.Items.OfType<ConsumableItem>().FirstOrDefault(i => i.Name == Name);
            if (item == null)
                PlayerInventory.Items.Add(this);
            else
                item.Count += Count;
        }
    }
}
