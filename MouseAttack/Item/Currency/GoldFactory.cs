using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Currency
{
    public class GoldFactory : ConsumableItemFactory
    {
        protected override CommonItem GetNewItem() =>
            new Gold();
    }
}
