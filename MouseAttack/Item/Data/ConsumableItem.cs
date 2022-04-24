using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Misc;
using System;

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

        int _value;
        public override int Value
        {
            get => _value * MonsterLevel;
            set => _value = value;
        }

        public override string TooltipType => TypeName;
        public override string DropText => Count.ToString();
        public override bool CanUse => ElapsedCooldown <= 0 && Count > 0;
        protected override bool ElapseTurnOnUse => false;

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
