using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Misc
{
    public class ItemEventArgs<T> : EventArgs where T : CommonItem
    {
        public T Item { get; private set; }
        public ItemEventArgs(T item) =>
            Item = item;
    }

    public class CommonItemEventArgs : ItemEventArgs<CommonItem>
    {
        public CommonItemEventArgs(CommonItem item) : base(item) { }
    }

    public class CommonEquipEventArgs : ItemEventArgs<CommonEquip>
    {
        public CommonEquipEventArgs(CommonEquip item) : base(item) { }
    }

    public class CommonSkillEventArgs : ItemEventArgs<CommonSkill>
    {
        public CommonSkillEventArgs(CommonSkill item) : base(item) { }
    }
}
