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
using System.Windows.Input;

namespace MouseAttack.Entity.Player.Inventory
{
    public class PlayerInventory : ObservableNode, ISharable, IInitializable
    {
        public event EventHandler Initialized;
        public event EventHandler<CommonItemEventArgs> Added;
        public event EventHandler<CommonItemEventArgs> Removed;
        public event EventHandler<CommonItemEventArgs> SlotChanged;

        List<CommonItem> _items = new List<CommonItem>();
        public ReadOnlyCollection<CommonItem> Items { get; private set; }

        public IEnumerable<ConsumableItem> Consumables => _items.OfType<ConsumableItem>();

        public List<ConsumableItem> CreatedConsumables = new List<ConsumableItem>();

        [Export]
        NodePath MainAttackFactoryPath { get; set; } = "";
        [Export]
        NodePath GoldFactoryPath { get; set; } = "";
        public DamageSkill MainAttack { get; private set; }
        public Gold Gold { get; private set; }

        readonly Dictionary<Type, ICommand> _addCommandMap = new Dictionary<Type, ICommand>();

        public PlayerInventory() =>
            TreeSharer.RegistryNode(this);


        public override void _Ready()
        {
            Gold = GetNode<GoldFactory>(GoldFactoryPath).CreateItem<Gold>();
            _items.Add(MainAttack = GetNode<DamageSkillFactory>(MainAttackFactoryPath).CreateItem<DamageSkill>());
            Items = new ReadOnlyCollection<CommonItem>(_items);
            _addCommandMap.Add(typeof(CommonItem), new AddCommonItemCommand(this));
            _addCommandMap.Add(typeof(ConsumableItem), new AddConsumableItemCommand(this));

            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public void Add(CommonItem item) =>
            GetAddCommand(item.GetType()).Execute(item);

        ICommand GetAddCommand(Type type)
        {
            if (type == null)
                return _addCommandMap[typeof(CommonItem)];
            if (!_addCommandMap.ContainsKey(type))
                return GetAddCommand(type.BaseType);
            return _addCommandMap[type];
        }


        // Called by the Iventory Add Command
        public void OnItemAdded(CommonItem item)
        {
            _items.Add(item);
            Added?.Invoke(this, new CommonItemEventArgs(item));
            item.PropertyChanged += OnItemPropertyChanged;
        }

        public void Remove(CommonItem item)
        {
            item.IsSlotted = false;
            item.PropertyChanged -= OnItemPropertyChanged;
            _items.Remove(item);
            Removed?.Invoke(this, new CommonItemEventArgs(item));
        }       

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CommonItem.IsSlotted))
                return;
            SlotChanged?.Invoke(this, new CommonItemEventArgs(sender as CommonItem));
        }
    }
}
