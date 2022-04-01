using Godot;
using MouseAttack.Entity.Player.UI;
using MouseAttack.GUI;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class CommonItem : Observable
    {
        public virtual string Name { get; protected set; }
        public virtual int Value { get; protected set; }
        public PackedScene FloatingLabelDropScene { get; private set; }
        public PackedScene IconScene { get; private set; }
        public PackedScene TooltipScene { get; private set; }
        public Texture IconTexture { get; private set; }

        protected virtual string DropText => Name;
        protected Random Random => TreeSharer.GetNode<Stage>().Random;

        bool _isSlotted = false;
        public bool IsSlotted 
        {
            get => _isSlotted;
            set
            {
                if (value == _isSlotted)
                    return;

                _isSlotted = value;
                OnPropertyChanged();
            }
        }
        public virtual string Tooltip => $"Value: {Value}";

        public virtual void ItemDropped(int monsterLevel) { }

        public virtual Icon GetSlotIcon()
        {
            Icon icon = IconScene.Instance<Icon>();
            icon.Texture = IconTexture;
            return icon;
        }

        public virtual Icon GetDropIcon()
        {
            Icon icon = GetSlotIcon();
            icon.BorderColor = Colors.Transparent;
            icon.BgColor = Colors.Transparent;
            return icon;
        }

        public virtual TooltipPanel GetTooltip()
        {
            TooltipPanel tooltipPanel = TooltipScene.Instance<TooltipPanel>();
            tooltipPanel.ItemName = Name;
            tooltipPanel.ItemStats = Tooltip;
            tooltipPanel.Icon = GetSlotIcon();
            return tooltipPanel;
        }


        public virtual FloatingLabel GetFloatingDropLabel()
        {
            FloatingLabel floatingLabel = FloatingLabelDropScene.Instance<FloatingLabel>();            
            floatingLabel.Icon = GetDropIcon();
            floatingLabel.Text = DropText;
            return floatingLabel;
        }
    }
}