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
        [Export]
        int _minBaseValue = 1;
        [Export]
        int _maxBaseValue = 3;

        protected override string DropText => Count.ToString();

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void ItemDropped(int monsterLevel)
        {
            Count = Random.Next(_minBaseValue * monsterLevel, _maxBaseValue * monsterLevel);
            PlayerInventory.Gold.Count += Count;
        }
    }
}
