using Godot;
using MouseAttack.Equip.ActionMenu;
using MouseAttack.Equip.Data;
using MouseAttack.Item.ActionMenu;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Provider
{
    public class ActionMenuProvider : ItemResourceProvider<Type>
    {
        protected override Dictionary<Type, Type> ResourceMap { get; set; } = new Dictionary<Type, Type>()
        {
            { typeof(CommonItem), typeof(ItemActionMenu) },
            { typeof(CommonEquip), typeof(EquipActionMenu) },
        };

        protected override Type DefaultResource => typeof(ItemActionMenu);

        public ItemActionMenu GetActionMenu(CommonItem item)
        {
            Type type = GetResource(item);
            ItemActionMenu itemActionMenu = (ItemActionMenu)Activator.CreateInstance(type);
            itemActionMenu.Item = item;
            return itemActionMenu;
        }
    }
}
