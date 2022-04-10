using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item;
using MouseAttack.Item.Data;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Equip.Tooltip
{
    public class EquipTooltipPanel : PanelContainer, IItemView
    {
        [Export]
        NodePath ItemTooltipPanel1Path { get; set; }
        [Export]
        NodePath ItemTooltipPanel2Path { get; set; }     
        [Export]
        NodePath SeparatorPath { get; set; }

        PlayerEquip PlayerEquip => TreeSharer.GetNode<PlayerEquip>();
        public void SetItem(CommonItem item) =>
            SetItem(item as CommonEquip);

        async public void SetItem(CommonEquip equip)
        {
            await ToSignal(this, Signals.Ready);
            Hide();

            ItemTooltipPanel itemTooltipPanel1 = GetNode<ItemTooltipPanel>(ItemTooltipPanel1Path);
            var equipedEquip = PlayerEquip.GetEquip(equip.Type);

            itemTooltipPanel1.SetupItem(equip);
            if (equip.IsKnown)
                itemTooltipPanel1.SetTooltipInfo(equip.GetTooltipInfo(equipedEquip));
            else
                itemTooltipPanel1.SetUnknownInfo();

            if (equip.IsSlotted)
                itemTooltipPanel1.SetAsEquiped();

            if (equipedEquip != null && equipedEquip != equip)
            {
                ItemTooltipPanel itemTooltipPanel2 = GetNode<ItemTooltipPanel>(ItemTooltipPanel2Path);
                itemTooltipPanel2.SetupItem(equipedEquip);
                itemTooltipPanel2.SetTooltipInfo(equipedEquip.GetTooltipInfo(equip));
                itemTooltipPanel2.Show();
                if (equipedEquip.IsSlotted)
                    itemTooltipPanel2.SetAsEquiped();

                GetNode<Control>(SeparatorPath).Show();
            }


            // Update Position
            await this.SkipNextFrame();

            RectGlobalPosition = TreeSharer
                .GetNode<PlayArea>()
                .ClampPosition(RectGlobalPosition, RectSize);
            Show();
        }
            
    }
}
