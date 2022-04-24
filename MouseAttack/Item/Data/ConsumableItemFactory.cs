using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class ConsumableItemFactory : UsableItemFactory
    {
        [Export]
        public int MinBaseValue { get; private set; } = 1;
        [Export]
        public int MaxBaseValue { get; private set; } = 3;

        public override T CreateItem<T>(int monsterLevel = 1)
        {
            var item = base.CreateItem<ConsumableItem>(monsterLevel);
            item.Count += Random.Next(MinBaseValue, MaxBaseValue);
            return item as T;
        }
    }
}
