using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Tooltip
{
    public interface ITooltipPanel
    {
        void SetItem(CommonItem item);
    }
}
