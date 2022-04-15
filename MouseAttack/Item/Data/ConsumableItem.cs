using Godot;
using MouseAttack.Entity;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class ConsumableItem : UsableItem
    {
        const string TypeName = "Consumable";
        protected override Action ApplyAction => Use;

        int _count = 0;
        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Value));
            }
        }

        public override string TooltipType => TypeName;
        public override string DropText => Count.ToString();
        public override bool CanUse => ElapsedCooldown <= 0 && Count > 0;

        protected PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public override void Use()
        {
            base.Use();
            Count--;
            if (Count <= 0)
                PlayerInventory.Remove(this);
        }
    }
}
