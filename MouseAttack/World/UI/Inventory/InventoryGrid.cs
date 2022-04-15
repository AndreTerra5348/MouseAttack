using Godot;
using MouseAttack.Entity.Player.Inventory;
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


namespace MouseAttack.World.UI.Inventory
{
    public enum GridType
    {
        All,
        Equip,
        Skill,
        Consumable
    }
    public class InventoryGrid : GridContainer
    {
        [Export]
        PackedScene _slotScene = null;
        [Export]
        GridType _gridType = GridType.All;

        readonly Dictionary<GridType, Type> _typeMap = new Dictionary<GridType, Type>()
        {
            { GridType.All, typeof(CommonItem) },
            { GridType.Equip, typeof(CommonEquip) },
            { GridType.Skill, typeof(CommonSkill) },
            { GridType.Consumable, typeof(ConsumableItem) }
        };

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void _Ready()
        {
            AddAll();

            PlayerInventory.Added += (s, e) => AddItem(e.Item);
            PlayerInventory.Removed += (s, e) => RemoveItem(e.Item);
        }

        private bool IsItemValid(CommonItem item) =>
            _typeMap[_gridType].IsAssignableFrom(item.GetType());

        private void AddAll()
        {
            foreach (CommonItem item in PlayerInventory.Items)
            {
                AddItem(item);
            }
        }

        private void RemoveItem(CommonItem item)
        {
            if (!IsItemValid(item))
                return;
            foreach (InventorySlot slot in GetChildren())
            {
                if (slot.Item == item)
                {
                    slot.UnsetItem();
                    slot.QueueFree();
                    break;
                }
            }
        }

        private void AddItem(CommonItem item)
        {
            if (!IsItemValid(item))
                return;

            InventorySlot slot = _slotScene.Instance<InventorySlot>();
            AddChild(slot);
            slot.Item = item;
        }
    }
}