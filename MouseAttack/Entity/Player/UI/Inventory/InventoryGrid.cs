using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI.Inventory
{
    public enum GridType
    {
        All,
        Equip,
        Skill
    }
    public class InventoryGrid : GridContainer
    {
        [Export]
        PackedScene _slotScene = null;
        [Export]
        int _startSlotCount = 25;
        [Export]
        GridType _gridType = GridType.All;

        readonly Dictionary<GridType, Type> _typeMap = new Dictionary<GridType, Type>()
        {
            { GridType.All, typeof(CommonItem) },
            { GridType.Equip, typeof(CommonEquip) },
            { GridType.Skill, typeof(CommonSkill) }
        };

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void _Ready()
        {
            for(int i = 0; i < _startSlotCount; i++)
            {
                AddNewSlot(null);
            }
            PlayerInventory.Items.CollectionChanged += (s, e) =>
            {
                if (e.Action != NotifyCollectionChangedAction.Add)
                    return;

                foreach (CommonItem item in e.NewItems.OfType<CommonItem>())
                {
                    if (!_typeMap[_gridType].IsAssignableFrom(item.GetType()))
                        continue;
                    Add(item);
                }
            };
        }

        public void Add(CommonItem item)
        {
            foreach (InventorySlot slot in GetChildren())
            {
                if (!slot.IsEmpty)
                    continue;
                slot.Item = item;
                item.Bind(nameof(CommonItem.IsSlotted), slot, nameof(Button.Disabled));
                return;
            }
            AddNewSlot(item);
        }

        private void AddNewSlot(CommonItem item)
        {
            InventorySlot slot = _slotScene.Instance<InventorySlot>();
            AddChild(slot);
            slot.Item = item;
        }
    }
}
