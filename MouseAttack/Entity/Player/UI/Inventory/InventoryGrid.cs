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
            int childCount = GetChildCount();
            for (int i = 0; i < _startSlotCount - childCount; i++)
            {
                AddNewSlot(null);
            }

            foreach (CommonItem item in PlayerInventory.Items)
            {
                AddItem(item);
            }

            PlayerInventory.Added += (s, e) => AddItem(e.Item);
            PlayerInventory.Removed += (s, e) => RemoveItem(e.Item);
        }

        private bool IsItemValid(CommonItem item) =>
            _typeMap[_gridType].IsAssignableFrom(item.GetType());

        private void RemoveItem(CommonItem item)
        {
            if (!IsItemValid(item))
                return;
            foreach (InventorySlot slot in GetChildren())
            {
                if (slot.IsEmpty || slot.Item != item)
                    continue;
                slot.UnsetItem();
                if(slot.GetIndex() > _startSlotCount)
                    slot.Hide();
                break;
            }
        }

        public void AddItem(CommonItem item)
        {
            if (!IsItemValid(item))
                return;
            foreach (InventorySlot slot in GetChildren())
            {
                if (!slot.IsEmpty)
                    continue;
                slot.Item = item;
                slot.Show();
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
