using Godot;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Item.Misc;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player
{
    public class PlayerEquip : ObservableNode, ISharable
    {
        public event EventHandler<CommonEquipEventArgs> SlotChanged;

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        public PlayerEquip() =>
            TreeSharer.RegistryNode(this);

        Dictionary<EquipType, CommonEquip> _equipped = new Dictionary<EquipType, CommonEquip>()
        {
            { EquipType.Offensive, null },
            { EquipType.Defensive, null },
            { EquipType.Special, null }
        };

        public override void _Ready()
        {
            PlayerInventory.SlotChanged += (s, e) => 
                OnItemSlotChanged(e.Item as CommonEquip);
        }

        public CommonEquip GetEquip(EquipType type) =>
            _equipped[type];

        public bool IsTypeEquipped(EquipType type) =>
            _equipped[type] != null;

        public void Unslot(EquipType type) =>
            _equipped[type].IsSlotted = false;

        private void OnItemSlotChanged(CommonEquip equip)
        {
            if (equip == null)
                return;

            // Update Equip Stats
            UpdateStats(equip.PrimaryStats, equip.IsSlotted);
            foreach (EquipStats stats in equip.SecondaryStats)
            {
                UpdateStats(stats, equip.IsSlotted);
            }

            _equipped[equip.Type] = equip.IsSlotted ? equip : null;

            SlotChanged?.Invoke(this, new CommonEquipEventArgs(equip));
        }

        private void UpdateStats(EquipStats stats, bool isSlotted) =>
            Character.SetAlteredPercentage(stats.Type, isSlotted ? stats.Percentage : -stats.Percentage);

        
    }
}
