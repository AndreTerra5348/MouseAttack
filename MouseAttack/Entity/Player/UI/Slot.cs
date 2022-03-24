using Godot;
using MouseAttack.Entity.Player.UI.Skill;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public abstract class Slot<T> : Button, INotifyPropertyChanged where T : CommonItem
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [Export]
        NodePath _iconContainerPath = "";
        CenterContainer _iconContainer;
        Control _currentIcon;

        [Export]
        protected bool RemoveOnDrag { get; set; } = false;

        T _item;
        public T Item
        {
            get => _item;
            set
            {
                if (_item == value)
                    return;
                _item = value;
                OnPropertyChanged();

                // Remove old Icon
                if (_currentIcon != null)
                {
                    _iconContainer.RemoveChild(_currentIcon);
                    _currentIcon = null;
                }

                if (_item == null)
                    return;

                // Set new Icon and tooltip
                _iconContainer.AddChild(_currentIcon = _item.NewIcon);
                HintTooltip = _item.Tooltip;
            }
        }
        public override void _Ready() =>
            _iconContainer = GetNode<CenterContainer>(_iconContainerPath);        

        public override bool CanDropData(Vector2 position, object data) =>
            data is T;

        public override void DropData(Vector2 position, object data) =>
            Item = data as T;

        public override object GetDragData(Vector2 position)
        {
            SetDragPreview(new DragPreview(Item.NewIcon));
            var data = Item;
            if(RemoveOnDrag)
                RemoveItem();
            return data;
        }

        public override Control _MakeCustomTooltip(string forText) =>
            this.MakeCustomTooltip(forText);

        protected void RemoveItem() =>
            Item = null;

    }
}
