using Godot;
using MouseAttack.Entity.Player.UI;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class CommonItem : ObservableResource
    {
        [Export]
        public virtual string Name { get; protected set; }
        [Export]
        public virtual int Value { get; protected set; }
        [Export]
        protected virtual int DropRate { get; set; }
        int _count = 0;
        [Export]
        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;
                OnPropertyChanged();
            }
        }

        [Export]
        protected PackedScene FloatingLabelDropScene { get; set; }        
        protected virtual string DropText => Name;
        protected Random Random { get; private set; } = new Random();
        public virtual bool Dropped => DropRate > Random.Next(100);
        [Export]
        protected PackedScene IconScene { get; set; }        

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

        public abstract void ItemDropped(int monsterLevel);

        public virtual string Tooltip => $"Value: {Value}";

        public virtual Control GetIcon()
            => IconScene.Instance<Control>();        

        public virtual FloatingLabel GetFloatingDropLabel()
        {
            FloatingLabel floatingLabel = FloatingLabelDropScene.Instance<FloatingLabel>();
            floatingLabel.Icon = GetIcon();
            floatingLabel.Text = DropText;
            return floatingLabel;
        }
    }
}