using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
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

        
        public CommonItem Item { get; set; }

        const string SellTextFormat = "Sell: {0}g";

        PlayerInventory PlayerInventory => 
            TreeSharer.GetNode<PlayerInventory>();        

        public override void AddAction() =>
            AddAction(String.Format(SellTextFormat, Item.Value), Sell);

        void Sell()
        {
            PlayerInventory.Sold(Item);
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
            

        
    }
}
