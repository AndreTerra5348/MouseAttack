using Godot;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Currency
{
    public class GoldFactory : CommonItemFactory
    {
        [AssignTo(nameof(Gold.MinBaseValue))]
        [Export]
        public int MinBaseValue { get; private set; } = 1;
        [AssignTo(nameof(Gold.MaxBaseValue))]
        [Export]
        public int MaxBaseValue { get; private set; } = 3;
        [AssignTo(nameof(Gold.Count))]
        [Export]
        public int Count { get; private set; } = 0;

        protected override CommonItem GetNewItem() =>
            new Gold();
    }
}
