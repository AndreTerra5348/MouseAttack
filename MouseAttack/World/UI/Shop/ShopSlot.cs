using Godot;
using MouseAttack.Constants;
using MouseAttack.Item.Data;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.Misc;
using MouseAttack.Item.Provider;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Player.Inventory;

namespace MouseAttack.World.UI.Shop
{
    public class ShopSlot : PanelContainer
    {
        public event EventHandler ItemBought;
        const string TooltipPlaceholder = "placeholder";
        const string PriceFormat = "{0}g";

        [Export]
        NodePath ItemFactoriesPath { get; set; }
        [Export]
        NodePath IconContanerPath { get; set; }
        [Export]
        NodePath BuyButtonPath { get; set; }
        [Export]
        NodePath PriceLabelPath { get; set; }
        [Export]
        int ItemLevelRange { get; set; } = 5;
        [Export]
        int PriceRange { get; set; } = 75;

        List<CommonItemFactory> _itemFactories;
        CenterContainer _iconContaner;
        CommonItem _item;
        Button _buyButton;
        Label _priceLabel;
        int _price = 0;
        bool _onCooldown = false;

        Stage Stage => TreeSharer.GetNode<Stage>();
        IconProvider IconProvider => TreeSharer.GetNode<IconProvider>();
        TooltipProvider TooltipProvider => TreeSharer.GetNode<TooltipProvider>();
        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        DropLabelProvider DropLabelProvider => TreeSharer.GetNode<DropLabelProvider>();
        DragPreviewParent DragPreviewParent => TreeSharer.GetNode<DragPreviewParent>();


        public override void _Ready()
        {
            _buyButton = GetNode<Button>(BuyButtonPath);
            _buyButton.Connect(Signals.Pressed, this, nameof(OnBuyButtonPressed));

            _itemFactories = GetNode(ItemFactoriesPath)
                .GetChildren()
                .OfType<CommonItemFactory>()
                .ToList();

            _iconContaner = GetNode<CenterContainer>(IconContanerPath);

            _priceLabel = GetNode<Label>(PriceLabelPath);

            UpdateItem();
            PlayerInventory.Gold.Bind(nameof(ConsumableItem.Count), _buyButton, nameof(Button.Disabled),
                propertyConvertor: (value) => _onCooldown || (int)value < _price);
        }

        public void UpdateItem()
        {
            _onCooldown = false;
            var factory = _itemFactories.GetRandomElement(Stage.Random);
            int monsterLevel = (int)(Character.Level + Stage.Random.Next(-ItemLevelRange, ItemLevelRange));
            monsterLevel = Math.Max(1, monsterLevel);
            _item = factory.CreateItem<CommonItem>(monsterLevel);
            var icon = IconProvider.GetDefaultIcon(_item);
            _iconContaner.AddChild(icon);
            HintTooltip = TooltipPlaceholder;
            _price = _item.Value + Stage.Random.Next(-_item.Value, _item.Value) * (PriceRange / 100);
            _priceLabel.Text = String.Format(PriceFormat, _price);
            _buyButton.Disabled = PlayerInventory.Gold.Count < _price;
        }

        public void Reset()
        {
            _onCooldown = true;
            _iconContaner.GetChild(0).QueueFree();
            HintTooltip = String.Empty;
            _priceLabel.Text = String.Empty;
            _buyButton.Disabled = true;
        }

        public override Control _MakeCustomTooltip(string forText) =>
            TooltipProvider.GetTooltip(_item) as Control;

        private void OnBuyButtonPressed()
        {
            _item.IsKnown = true;
            PlayerInventory.Add(_item);
            PlayerInventory.Gold.Count -= _price;
            var dropLabel = DropLabelProvider.GetLabel(_item);
            dropLabel.Position = RectGlobalPosition;
            DragPreviewParent.AddChild(dropLabel);
            ItemBought?.Invoke(this, EventArgs.Empty);
        }

        public void SetBuyButtondisabled(bool disabled) =>
            _buyButton.Disabled = disabled;
    }
}
