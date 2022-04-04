using Godot;
using MouseAttack.Constants;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player
{
    public class PlayerInventory : ObservableNode, ISharable, IInitializable
    {
        public event EventHandler Initialized;
        public ObservableCollection<CommonItem> Items { get; set; } = new ObservableCollection<CommonItem>();
        [Export]
        NodePath MainAttackFactoryPath { get; set; } = "";
        [Export]
        NodePath GoldFactoryPath { get; set; } = "";
        public DamageSkill MainAttack { get; private set; }


        Dictionary<EquipType, CommonEquip> equipMap = new Dictionary<EquipType, CommonEquip>()
        {
            { EquipType.Offensive, null },
            { EquipType.Defensive, null },
            { EquipType.Special, null }
        };

        public PlayerInventory() =>
            TreeSharer.RegistryNode(this);

        public CommonEquip GetEquip(EquipType type) =>
            equipMap[type];

        public void EquipSlotStateChanged(CommonEquip equip, bool isSlotted)
        {
            equipMap[equip.Type] = isSlotted ? equip : null;
            OnPropertyChanged(nameof(equip.Type));
        }

        public override void _Ready()
        {
            Items.Add(GetNode<ConsumableItemFactory>(GoldFactoryPath).CreateItem<ConsumableItem>());
            Items.Add(MainAttack = GetNode<DamageSkillFactory>(MainAttackFactoryPath).CreateItem<DamageSkill>());
            Initialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
