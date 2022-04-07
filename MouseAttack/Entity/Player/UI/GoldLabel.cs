using Godot;
using MouseAttack.Extensions;
using MouseAttack.Item.Currency;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class GoldLabel : Label
    {
        PlayerInventory Inventory => TreeSharer.GetNode<PlayerInventory>();
        Gold Gold => Inventory.Gold;
        public override void _Ready() =>
            Gold.Bind(nameof(Item.Currency.Gold.Count), this, nameof(Label.Text));
    }
}
