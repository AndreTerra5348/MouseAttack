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
        Label CountLabel { get; set; }
        new ConsumableItem Item => base.Item as ConsumableItem;

        public override void _Ready()
        {
            base._Ready();
            CountLabel = GetNode<Label>(CountLabelPath);
            UpdateLabel();
            Item.PropertyChanged += OnItemPropertyChanged;
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConsumableItem.Count))
                UpdateLabel();
        }

        private void UpdateLabel() =>
            CountLabel.Text = Item.Count.ToString();

        public override void _ExitTree() =>
            Item.PropertyChanged -= OnItemPropertyChanged;

    }
}
