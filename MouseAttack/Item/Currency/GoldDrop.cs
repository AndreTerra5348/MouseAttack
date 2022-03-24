using MouseAttack.Entity.Player;
using MouseAttack.Item.Data;
using MouseAttack.Item.Drop;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Gold
{
    public class GoldDrop : CommonDrop
    {
        protected override void ItemPickup() =>
            PlayerInventory.Gold.Add(ItemData.Count);
    }
}
