using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Consumable
{
    public class PotionItemFactory : ConsumableItemFactory
    {
        [AssignTo(nameof(PotionItem.Type))]
        [Export]
        public ResourceType Type { get; private set; }
        [AssignTo(nameof(PotionItem.Amount))]
        [Export]
        public int Amount { get; private set; }

        protected override CommonItem GetNewItem() =>
            new PotionItem();
    }
}
