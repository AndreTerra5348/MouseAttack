using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public class ConsumableItemFactory : CommonItemFactory
    {
        [AssignTo(nameof(ConsumableItem.MinBaseValue))]
        [Export]
        public int MinBaseValue { get; private set; } = 1;
        [AssignTo(nameof(ConsumableItem.MaxBaseValue))]
        [Export]
        public int MaxBaseValue { get; private set; } = 3;
        [AssignTo(nameof(ConsumableItem.Count))]
        [Export]
        public int Count { get; private set; } = 0;
        protected override CommonItem GetNewItem() =>
            new ConsumableItem();
    }
}
