using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item
{
    public interface IItemView
    {
        void SetItem(CommonItem item);
    }
}
