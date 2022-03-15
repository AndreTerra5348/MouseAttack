using Godot;
using MouseAttack.Action;
using MouseAttack.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class DragAndDropData<T> : Node
    {
        public T Data { get; private set; }
        public event EventHandler Dropped;

        public DragAndDropData(T data)
        {
            Data = data;
        }

        public void DataDropped() => Dropped?.Invoke(this, EventArgs.Empty);
    }
    public class ActionSlot : Button, INotifyPropertyChanged
    {
        [Export]
        NodePath _cooldownPath = "";
        CooldownTextureProgress _cooldown;
        [Export]
        NodePath _iconPath = "";
        TextureRect _icon;

        public new Texture Icon
        {
            get => _icon.Texture;
            set => _icon.Texture = value;
        }

        CommonAction _action;
        public CommonAction Action
        {
            get => _action;
            private set
            {
                if (_action == value)
                    return;
                _action = value;
                OnPropertyChanged();
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        public override void _Ready()
        {
            _cooldown = GetNode<CooldownTextureProgress>(_cooldownPath);
            _icon = GetNode<TextureRect>(_iconPath);
            RemoveAction();
        }

        public override bool CanDropData(Vector2 position, object data) => 
            data is DragAndDropData<CommonAction>;

        public override void DropData(Vector2 position, object data) =>
            SetAction((data as DragAndDropData<CommonAction>).Data);

        public override object GetDragData(Vector2 position)
        {
            SetDragPreview(new ActionDragPreview(_icon.Duplicate() as TextureRect));
            var data = new DragAndDropData<CommonAction>(Action);
            RemoveAction();
            return data;
        }

        public override Control _MakeCustomTooltip(string forText) => 
            this.MakeCustomTooltip(forText);

        public void Use(float timeout) => 
            _cooldown.StartCooldown(timeout);

        public void SetAction(CommonAction action)
        {
            Action = action;
            Icon = Action.Icon;
            HintTooltip = Action.Tooltip;
        }

        private void RemoveAction()
        {
            Action = null;
            Icon = null;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
