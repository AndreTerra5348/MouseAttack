using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World;
using MouseAttack.World.UI.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.ActionMenu
{
    public class ItemActionMenu : ActionMenuBase
    {
        [Export]
        PackedScene DropLabelScene { get; set; }
        [Export]
        Texture GoldTexture { get; set; }

        DragPreviewParent DragPreviewParent => 
            TreeSharer.GetNode<DragPreviewParent>();

        ShopPopupProvider SellPopupProvider => 
            TreeSharer.GetNode<ShopPopupProvider>();

        public CommonItem Item { get; set; }

        const string SellTextFormat = "Sell: {0}g";

        public override void AddAction() =>
            AddAction(String.Format(SellTextFormat, Item.Value), Sell);

        void Sell()
        {
            CommonShopPopup sellPopup = SellPopupProvider.GetSellPopup(Item);
            sellPopup.Item = Item;
            sellPopup.ConfirmationAction = (value) =>
                SpawnFloatingLabel(value.ToString());

            AddChild(sellPopup);
            sellPopup.Popup();       
        }

        void SpawnFloatingLabel(string text)
        {
            var dropLabel = DropLabelScene.Instance<DropLabel>();
            dropLabel.IconTexture = GoldTexture;
            dropLabel.Text = text;
            dropLabel.Position = RectGlobalPosition;
            DragPreviewParent.AddChild(dropLabel);
        }
            

        
    }
}
