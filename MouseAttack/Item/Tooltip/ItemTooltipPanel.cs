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
    public class ItemTooltipPanel : PanelContainer, IItemView
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

        const string UnknownLabel = "???";
        public void SetupItem(CommonItem item)
        {
            NameLabel.Text = item.IsKnown ? item.Name : UnknownLabel;
            TypeLabel.Text = item.TooltipType;
            IconContainer.AddChild(IconProvider.GetIcon(item));
            NameLabel.AddColorOverride(Overrides.FontColor, item.Color);
        }

        public void SetAsEquiped() =>
            EquipedLabel.Show();

        public void SetTooltipInfo(Stack<TooltipInfo> tooltipInfo)
        {
            foreach (TooltipInfo info in tooltipInfo)
            {
                AddInfoLabel(info.Text, info.Color);
            }
        }

        public void SetUnknownInfo() =>
            AddInfoLabel(UnknownLabel, Colors.White);

        protected void AddInfoLabel(string text, Color color)
        {
            var label = new Label();
            label.Text = text;
            label.AddColorOverride(Overrides.FontColor, color);
            InfoLabelContainer.AddChild(label);
        }

        async public void SetItem(CommonItem item)
        {
            await ToSignal(this, Signals.Ready);

            Hide();
            SetupItem(item);
            if (item.IsKnown)
                SetTooltipInfo(item.GetTooltipInfo());
            else
                SetUnknownInfo();

            // Update position
            await this.SkipNextFrame();

            RectGlobalPosition = TreeSharer
                .GetNode<PlayArea>()
                .ClampPosition(RectGlobalPosition, RectSize);
            Show();
        }

            
    }
}
