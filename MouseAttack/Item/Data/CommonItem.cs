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
    public interface ISellable
    {
        int Price { get; }
    }

    public class CommonItem : ObservableResource
    {
        const string DropLabelFormat = "+{0} {1}";
        [Export]
        PackedScene FloatingLabelDropScene { get; set; }
        [Export]
        public virtual int DropRate { get; protected set; }
        [Export]
        public virtual string Name { get; protected set; }
        [Export]
        PackedScene IconScene { get; set; }

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
        
        public virtual string Tooltip { get; }

        public void Add(int count = 1) => Count += count;
        public void Remove(int count = 1) => Count -= count;

        public T GetIconInstance<T>() where T : Control
            => IconScene.Instance<T>();

        public FloatingLabel GetFloatingDropLabel(Vector2 position, int count = 1)
        {
            FloatingLabel floatingLabel = FloatingLabelDropScene.Instance<FloatingLabel>();
            floatingLabel.Text = String.Format(DropLabelFormat, count, Name);
            floatingLabel.Position = position;
            return floatingLabel;
        }
    }
}