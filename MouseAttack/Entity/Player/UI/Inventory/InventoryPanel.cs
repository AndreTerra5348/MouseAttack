using Godot;
using MouseAttack.Constants;
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
    public class InventoryPanel : PanelBase
    {
        [Export]
        NodePath _goldLabelPath = "";

        protected override string OpenInputAction => InputAction.InventoryPanel;

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

        public override void _Ready()
        {
            Label goldLabel = GetNode<Label>(_goldLabelPath);
            PlayerInventory.Gold.Bind(nameof(CommonItem.Count), goldLabel, nameof(Label.Text), setImmediate: true);
            PlayerInventory.AddMainAttack();
        }
    }
}
