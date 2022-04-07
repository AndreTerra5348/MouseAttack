﻿using Godot;
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
        public string Name { get; protected set; }
        public virtual int Value { get; protected set; }
        public List<Texture> IconTexture { get; private set; }
        public virtual Color Color { get; protected set; } = new Color("a8a8a8");
        public int MonsterLevel { get; protected set; } = 1;
        public virtual bool IsDraggable => true;
        public virtual string DropText => Name;
        public virtual string TooltipType { get; protected set; } = "Misc";
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

        public virtual void ItemDropped(int monsterLevel) { }
        public virtual Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = new Stack<TooltipInfo>();
            tooltipInfo.Push(new TooltipInfo($"Value: {Value}", Colors.Yellow));
            return tooltipInfo;
        }           

        public Texture GetIconTexture() =>
            IconTexture[Math.Max(MonsterLevel - 1, IconTexture.Count - 1)];
    }
}