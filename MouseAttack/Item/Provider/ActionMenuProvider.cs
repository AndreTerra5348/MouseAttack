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
    public class ActionMenuProvider : ItemResourceProvider<PackedScene>
    {
        protected override Dictionary<Type, PackedScene> ResourceMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(CommonEquip), null },
        };

        protected override PackedScene DefaultResource => ItemActionMenu;

        [Export]
        PackedScene ItemActionMenu
        {
            get => ResourceMap[typeof(CommonItem)];
            set => ResourceMap[typeof(CommonItem)] = value;
        }

        [Export]
        PackedScene EquipActionMenu
        {
            get => ResourceMap[typeof(CommonEquip)];
            set => ResourceMap[typeof(CommonEquip)] = value;
        }

        public ItemActionMenu GetActionMenu(CommonItem item)
        {
            PackedScene actionMenuScene = GetResource(item.GetType());
            var itemActionMenu = actionMenuScene.Instance<ItemActionMenu>();
            itemActionMenu.Item = item;
            return itemActionMenu;
        }
    }
}
