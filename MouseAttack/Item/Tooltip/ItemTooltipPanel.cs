using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Item.Provider;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Tooltip
{
    public class ItemTooltipPanel : PanelContainer, ITooltipPanel
    {
        [Export]
        NodePath IconContainerPath { get; set; } = "";
        [Export]
        NodePath NameLabelPath { get; set; } = "";
        [Export]
        NodePath TypeLabelPath { get; set; } = "";
        [Export]
        NodePath EquipedLabelPath { get; set; }
        [Export]
        NodePath InfoLabelContainerPath { get; set; } = "";        

        Control IconContainer => GetNode<Control>(IconContainerPath);
        Label NameLabel => GetNode<Label>(NameLabelPath);
        Label TypeLabel => GetNode<Label>(TypeLabelPath);
        Label EquipedLabel => GetNode<Label>(EquipedLabelPath);
        Control InfoLabelContainer => GetNode<Control>(InfoLabelContainerPath);
        IconProvider IconProvider => TreeSharer.GetNode<IconProvider>();

        public void SetupItem(CommonItem item)
        {
            NameLabel.Text = item.Name;
            TypeLabel.Text = item.TooltipType;
            IconContainer.AddChild(IconProvider.GetSlotIcon(item));
            NameLabel.AddColorOverride(Overrides.FontColor, item.Color);
        }

        public void SetAsEquiped() =>
            EquipedLabel.Show();

        public void SetTooltipInfo(Stack<TooltipInfo> tooltipInfo)
        {
            foreach (TooltipInfo info in tooltipInfo)
            {
                var label = new Label();
                label.Text = info.Text;
                label.AddColorOverride(Overrides.FontColor, info.Color);
                InfoLabelContainer.AddChild(label);
            }
        }       

        async public void SetItem(CommonItem item)
        {
            await ToSignal(this, Signals.Ready);

            Hide();
            SetupItem(item);
            SetTooltipInfo(item.GetTooltipInfo());

            // Update position
            await this.SkipNextFrame();

            RectGlobalPosition = TreeSharer
                .GetNode<PlayArea>()
                .ClampPosition(RectGlobalPosition, RectSize);
            Show();
        }
            
    }
}
