using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Item.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Shop
{
    public class ShopPopupProvider : ItemResourceProvider<PackedScene>
    {
        protected override Dictionary<Type, PackedScene> ResourceMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(CommonEquip), null },
            { typeof(ConsumableItem), null },
        };

        protected override PackedScene DefaultResource => CommonPopupScene;

        [Export]
        PackedScene CommonPopupScene
        {
            get => ResourceMap[typeof(CommonItem)];
            set => ResourceMap[typeof(CommonItem)] = value;
        }
        [Export]
        PackedScene EquipPopupScene
        {
            get => ResourceMap[typeof(CommonEquip)];
            set => ResourceMap[typeof(CommonEquip)] = value;
        }
        [Export]
        PackedScene ConsumablePopupScene
        {
            get => ResourceMap[typeof(ConsumableItem)];
            set => ResourceMap[typeof(ConsumableItem)] = value;
        }

        public CommonShopPopup GetSellPopup(CommonItem item)
        {
            PackedScene sellPopupScene = GetResource(item.GetType());
            return sellPopupScene.Instance<CommonShopPopup>();
        }

    }
}
