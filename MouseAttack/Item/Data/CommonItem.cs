using Godot;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.World;
using System;
using System.Collections.Generic;

namespace MouseAttack.Item.Data
{
    public abstract class CommonItem : Observable
    {
        public virtual string Name { get; protected set; }
        public virtual int Value { get; set; }
        public List<Texture> IconTexture { get; private set; }
        public virtual Color Color { get; protected set; } = new Color("a8a8a8");
        public virtual bool IsDraggable => true;
        public virtual bool IsStorable => true;
        public virtual string DropText => Name;
        public virtual string TooltipType { get; protected set; } = "Misc";
        public virtual bool IsKnown { get; set; } = true;
        public int MonsterLevel { get; set; } = 1;
        protected Random Random => 
            TreeSharer.GetNode<Stage>().Random;

        protected GridController GridController => 
            TreeSharer.GetNode<GridController>();

        public CommonItem() =>
            GridController.RoundFinished += OnRoundFinished;

        ~CommonItem() =>
            GridController.RoundFinished -= OnRoundFinished;

        private void OnRoundFinished(object sender, EventArgs e) =>
            OnRoundFinished();

        protected virtual void OnRoundFinished() { }


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

        public virtual Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = new Stack<TooltipInfo>();
            tooltipInfo.Push(new TooltipInfo($"Value: {Value}", Colors.Yellow));
            return tooltipInfo;
        }           

        public Texture GetIconTexture() =>
            IconTexture[Math.Min(MonsterLevel - 1, IconTexture.Count - 1)];

    }
}