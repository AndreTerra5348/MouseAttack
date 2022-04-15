using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity;
using MouseAttack.Entity.Player;
using MouseAttack.Item.Data;
using MouseAttack.Item.Tooltip;
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
        public StatsType Type { get; private set; }

        int _amount = 0;
        public int Amount
        {
            get => _amount * MonsterLevel;
            private set => _amount = value;
        }

        int _value;
        public override int Value
        {
            get => _value * MonsterLevel;
            set => _value = value;
        }

        string _name = "";

        public override string Name
        {
            get => $"{_name} Lv. {MonsterLevel}";
            protected set => _name = value;
        }

        PlayerEntity PlayerEntity => 
            TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => 
            PlayerEntity.Character;

        public override Color Color => Colors.LightSalmon;
        public override string DropText => $"{Count} {Name}";

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Recharge: {Amount} {Enum.GetName(typeof(StatsType), Type)}", Color));
            return tooltipInfo;
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
