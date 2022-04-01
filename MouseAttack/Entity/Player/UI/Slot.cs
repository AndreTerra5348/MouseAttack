using Godot;
using MouseAttack.Entity.Player.UI.Inventory;
using MouseAttack.Entity.Player.UI.Skill;
using MouseAttack.Extensions;
using MouseAttack.GUI;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public class SlotDragData : Godot.Object
    {
        public readonly CommonItem Item;
        public readonly Slot SlotOrigin;

        public SlotDragData(CommonItem item, Slot slotOrigin)
        {
            Item = item;
            SlotOrigin = slotOrigin;
        }
    }

    public abstract class Slot : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [Export]
        NodePath _iconContainerPath = "";
        CenterContainer _iconContainer;
        [Export]
        PackedScene _tooltipPanel = null;

        Control _currentIcon;

        DragPreviewParent DragPreviewParent => TreeSharer.GetNode<DragPreviewParent>();        
        public bool IsEmpty => Item == null;

        CommonItem _item;
        public CommonItem Item
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
                HintTooltip = _item?.Tooltip;

                if (_item == null)
                    return;

                // Set new Icon
                _iconContainer.AddChild(_currentIcon = _item.GetSlotIcon());
            }
        }
        public override void _Ready() =>
            _iconContainer = GetNode<CenterContainer>(_iconContainerPath);        

        public override bool CanDropData(Vector2 position, object data) =>
            CanDropData(data as SlotDragData);

        public virtual bool CanDropData(SlotDragData data) =>
            data?.Item is CommonItem;

        public override void DropData(Vector2 position, object data)
        {
            SlotDragData slotDragData = data as SlotDragData;
            ItemDropped(slotDragData.Item);
            Item = slotDragData.Item;
            Item.IsSlotted = true;
            slotDragData.SlotOrigin.ItemDroppedAtAnotherSlot(this);
        }
            

        public override object GetDragData(Vector2 position)
        {
            if (Item == null)
                return null;
            DragPreviewParent.SetDragPreview(new DragPreview(Item.GetSlotIcon()));
            var data = new SlotDragData(Item, this);
            ItemDragged();
            return data;
        }
        public override Control _MakeCustomTooltip(string forText) =>
            Item.GetTooltip();


        protected virtual void ItemDragged() {}

        // Called before the item is asssigned
        protected virtual void ItemDropped(CommonItem item) { }

        /// <summary>
        /// Called by the slot that received the item dragged from this slot
        /// </summary>
        /// <param name="receiver">the receiver slot</param>
        public virtual void ItemDroppedAtAnotherSlot(Slot receiver) { }
        protected virtual void OnRightClick() { }

        

        protected void RemoveItem() =>
            Item = null;

        public override void _GuiInput(InputEvent @event)
        {
            InputEventMouseButton inputEvent = @event as InputEventMouseButton;
            if (inputEvent == null)
                return;
            if (inputEvent.ButtonIndex != (int)ButtonList.Right)
                return;

            OnRightClick();
        }

        
    }
}
