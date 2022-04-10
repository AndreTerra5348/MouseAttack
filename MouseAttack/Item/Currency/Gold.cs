using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Currency
{
    public class Gold : ConsumableItem
    {
        public override bool IsStorable => false;

        public override void ItemDropped(int monsterLevel) =>
            PlayerInventory.Gold.Count += (Count = GetRandomValue(monsterLevel));
    }
}
