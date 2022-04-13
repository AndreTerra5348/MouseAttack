using Godot;
using MouseAttack.Constants;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI.Shop
{
    public class ConsumableShopPopup : CommonShopPopup
    {
        const string SpinBoxSufixFormat = "/{0}";
        [Export]
        NodePath SpinBoxPath { get; set; } = "";
        SpinBox _spinBox;
        new ConsumableItem Item => base.Item as ConsumableItem;
        int SpinBoxCount => (int)_spinBox.Value;
        public override void _Ready()
        {
            base._Ready();
            _spinBox = GetNode<SpinBox>(SpinBoxPath);
            _spinBox.MaxValue = Item.Count;
            _spinBox.Suffix = string.Format(SpinBoxSufixFormat, Item.Count);
            _spinBox.Connect(Signals.ValueChanged, this, nameof(OnSpinBoxValueChanged));
        }

        private void OnSpinBoxValueChanged(float value) =>
            PriceLabel.Text = (Item.Value * value).ToString("0.0");

        protected override void Sold()
        {
            int value = Item.Value * SpinBoxCount;
            ConfirmationAction(value);
            Item.Count -= SpinBoxCount;
            AddGold(value);
            if (Item.Count <= 0)
                RemoveItem();
        }
    }
}
