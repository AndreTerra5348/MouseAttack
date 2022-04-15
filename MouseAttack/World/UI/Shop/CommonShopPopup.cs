using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Provider;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Shop
{
    public class CommonShopPopup : AcceptDialog
    {
        [Export]
        NodePath ItemLabelPath { get; set; } = "";
        [Export]
        NodePath ItemIconContainerPath { get; set; } = "";
        [Export]
        NodePath PriceLabelPath { get; set; } = "";

        protected Label PriceLabel { get; private set; }

        public Action<int> ConfirmationAction { get; set; }
        public CommonItem Item { get; set; }

        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();
        IconProvider IconProvider =>
            TreeSharer.GetNode<IconProvider>();
        public override void _Ready()
        {
            var itemLabel = GetNode<Label>(ItemLabelPath);
            itemLabel.Text = Item.Name;
            itemLabel.AddColorOverride(Overrides.FontColor, Item.Color);

            var itemIconContainer = GetNode<CenterContainer>(ItemIconContainerPath);
            CommonIcon icon = IconProvider.GetDefaultIcon(Item);
            itemIconContainer.AddChild(icon);

            var sellButton = GetOk();
            sellButton.RectMinSize = new Vector2(40, 20);
            sellButton.Text = "Sell";
            sellButton.Connect(Signals.Pressed, this, nameof(OnSellButtonPressed));

            PriceLabel = GetNode<Label>(PriceLabelPath);
            PriceLabel.Text = Item.Value.ToString();

            Connect(Signals.PopupHide, this, nameof(OnPopupHide));
        }

        private void OnPopupHide() =>
            QueueFree();


        private void OnSellButtonPressed()
        {
            Sold();
            OnPopupHide();
        }
            

        protected void AddGold(int value) =>
            PlayerInventory.Gold.Count += value;

        protected void RemoveItem() =>
            PlayerInventory.Remove(Item);

        protected virtual void Sold()
        {
            ConfirmationAction(Item.Value);
            AddGold(Item.Value);
            RemoveItem();
        }

        public virtual void Popup() =>
            PopupCentered();
    }
}
