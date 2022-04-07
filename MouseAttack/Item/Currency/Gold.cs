using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Currency
{
    public class Gold : ConsumableItem
    {
        public override void ItemDropped(int monsterLevel) =>
            PlayerInventory.Gold.Count += (Count = GetRandomValue(monsterLevel));
    }
}
