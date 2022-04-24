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
    public class StatsPotionFactory : ConsumableItemFactory
    {
        [AssignTo(nameof(StatsPotion.Type))]
        [Export]
        public StatsType Type { get; private set; }
        [AssignTo(nameof(StatsPotion.Percentage))]
        [Export]
        public float Percentage { get; private set; }
        protected override CommonItem GetNewItem() =>
            new StatsPotion();
    }
}
