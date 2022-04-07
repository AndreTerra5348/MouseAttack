using Godot;
using MouseAttack.Constants;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item.Currency;
using MouseAttack.Item.Data;
using MouseAttack.Item.Misc;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player
{    
    public class PlayerInventory : ObservableNode, ISharable, IInitializable
    {
        public event EventHandler Initialized;
        public event EventHandler<CommonItemEventArgs> Added;
        public event EventHandler<CommonItemEventArgs> Removed;
        public event EventHandler<CommonItemEventArgs> SlotChanged;

        List<CommonItem> _items = new List<CommonItem>();
        public ReadOnlyCollection<CommonItem> Items { get; private set; }
        public IEnumerable<CommonSkill> Skills => _items.OfType<CommonSkill>();
        public IEnumerable<ConsumableItem> Consumables => _items.OfType<ConsumableItem>();

        [Export]
        NodePath MainAttackFactoryPath { get; set; } = "";
        [Export]
        NodePath GoldFactoryPath { get; set; } = "";
        public DamageSkill MainAttack { get; private set; }
        public Gold Gold { get; private set; }

        public PlayerInventory()
        {
            TreeSharer.RegistryNode(this);
            Items = new ReadOnlyCollection<CommonItem>(_items);
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CommonItem.IsSlotted))
                return;

            SlotChanged?.Invoke(this, new CommonItemEventArgs(sender as CommonItem));
        }

        public void Add(CommonItem item)
        {
            _items.Add(item);
            Added?.Invoke(this, new CommonItemEventArgs(item));
            item.PropertyChanged += OnItemPropertyChanged;
        }
        

        public void Remove(CommonItem item)
        {
            item.IsSlotted = false;
            _items.Remove(item);
            Removed?.Invoke(this, new CommonItemEventArgs(item));
        }            

        public void Sold(CommonItem item)
        {
            Gold.Count += item.Value;
            Remove(item);
        }

        public void Bought(CommonItem item)
        {
            Gold.Count -= item.Value;
            Add(item);
        }

        public override void _Ready()
        {
            Gold = GetNode<GoldFactory>(GoldFactoryPath).CreateItem<Gold>();
            _items.Add(MainAttack = GetNode<DamageSkillFactory>(MainAttackFactoryPath).CreateItem<DamageSkill>());
            Initialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
