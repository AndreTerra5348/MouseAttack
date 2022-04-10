using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.ActionMenu
{
    public class ItemActionMenu : ActionMenuBase
    {
        [Export]
        PackedScene ConfirmationPanelScene { get; set; }
        [Export]
        PackedScene ConsumableSellPanelScene { get; set; }
        [Export]
        PackedScene DropLabelScene { get; set; }
        [Export]
        Texture GoldTexture { get; set; }

        DragPreviewParent DragPreviewParent => 
            TreeSharer.GetNode<DragPreviewParent>();
        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public CommonItem Item { get; set; }

        const string SellTextFormat = "Sell: {0}g";

        public override void AddAction()
        {
            if (Item.Value == 0)
                return;
            AddAction(String.Format(SellTextFormat, Item.Value), Sell);
        }

        void Sell()
        {
            PlayerInventory.Sold(Item);
            SpawnFloatingLabel(Item.Value.ToString());
            //switch (Item.GetType().Name)
            //{
            //    case nameof(CommonItem):
            //        PlayerInventory.Sold(Item);
            //        break;
            //    case nameof(ConsumableItem):
            //        // Show Consumable Sell Panel 
            //        break;
            //    case nameof(CommonEquip):
            //        // Show Confirmation Panel if it is Epic
            //        break;
            //}            
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
