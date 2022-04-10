using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Extensions;
using MouseAttack.Item.Currency;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI
{
    public class GoldLabel : Label
    {
        PlayerInventory Inventory => TreeSharer.GetNode<PlayerInventory>();
        Gold Gold => Inventory.Gold;
        public override void _Ready() =>
            Gold.Bind(nameof(Item.Currency.Gold.Count), this, nameof(Text));
    }
}
