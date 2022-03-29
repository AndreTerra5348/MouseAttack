using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.GUI
{
    public class TooltipPanel : PanelContainer
    {
        [Export]
        NodePath _iconContainerPath = "";
        CenterContainer _iconContainer;
        [Export]
        NodePath _nameLabelPath = "";
        Label _nameLabel;
        [Export]
        NodePath _statsLabelPath = "";
        Label _statsLabel;

        public string ItemName { get; set; }
        public string ItemStats { get; set; }
        public Control Icon { get; set; }

        async public override void _Ready()
        {
            _iconContainer = GetNode<CenterContainer>(_iconContainerPath);
            _iconContainer.AddChild(Icon);
            _nameLabel = GetNode<Label>(_nameLabelPath);
            _nameLabel.Text = ItemName;
            _statsLabel = GetNode<Label>(_statsLabelPath);
            _statsLabel.Text = ItemStats;

            await this.SkipNextFrame();
            RectGlobalPosition = TreeSharer
                .GetNode<PlayArea>()
                .ClampPosition(RectGlobalPosition, RectSize);
        }
    }
}
