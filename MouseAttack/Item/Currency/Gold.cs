using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Currency
{
    public class Gold : CommonItem
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
        protected override string DropText => Count.ToString();

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void ItemDropped(int monsterLevel)
        {
            Count = Random.Next(MinBaseValue * monsterLevel, MaxBaseValue * monsterLevel);
            PlayerInventory.Gold.Count += Count;
        }
    }
}
