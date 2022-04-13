using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Icon
{
    public class ConsumableIcon : CommonIcon
    {
        [Export]
        NodePath CountLabelPath { get; set; } = "";
        Label _countLabel;
        ConsumableItem _item;

        public override void SetItem(CommonItem item)
        {
            base.SetItem(item);
            SetItem(item as ConsumableItem);
        }
        public async void SetItem(ConsumableItem item)
        {
            await ToSignal(this, Signals.Ready);
            _item = item;
            _countLabel = GetNode<Label>(CountLabelPath);
            UpdateLabel();
            _item.PropertyChanged += OnItemPropertyChanged;
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ConsumableItem.Count))
                return;
            UpdateLabel();
        }

        private void UpdateLabel() =>
            _countLabel.Text = _item.Count.ToString();

        public override void _ExitTree() =>
            _item.PropertyChanged -= OnItemPropertyChanged;

    }
}
