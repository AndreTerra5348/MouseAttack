using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Consumable
{
    public class PotionItem : ConsumableItem
    {
        const string DropTextFormat = "{0} {1}";
        public StatsType Type { get; private set; }
        public int Amount { get; private set; }

        PlayerEntity PlayerEntity => 
            TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => 
            PlayerEntity.Character;

        public override Color Color => Colors.LightSalmon;
        public override string DropText => String.Format(DropTextFormat, DroppedCount, Name);

        public override void ItemDropped(int monsterLevel)
        {
            base.ItemDropped(monsterLevel);
            Amount *= monsterLevel;
        }

        public override void Use()
        {
            base.Use();

            ResourcePool resource = Character.GetResourcePool(Type);
            resource.Regenerate(Amount);

            SpawnFloatingLabel(PlayerEntity, Amount.ToString());
        }
    }
}
