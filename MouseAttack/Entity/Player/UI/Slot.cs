using Godot;
using MouseAttack.Entity.Player.UI.Inventory;
using MouseAttack.Entity.Player.UI.Skill;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Provider;
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
    public abstract class Slot : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [Export]
        NodePath _iconContainerPath = "";
        CenterContainer _iconContainer;

        const string TooltipPlaceholder = "placeholder";

        DragPreviewParent DragPreviewParent => TreeSharer.GetNode<DragPreviewParent>();
        IconProvider IconProvider => TreeSharer.GetNode<IconProvider>();
        TooltipProvider TooltipProvider => TreeSharer.GetNode<TooltipProvider>();
        ActionMenuProvider ActionMenuProvider => TreeSharer.GetNode<ActionMenuProvider>();

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
                if (_iconContainer.GetChildCount() > 0)
                {                    
                    foreach(Node child in _iconContainer.GetChildren())
                    {
                        child.QueueFree();
                    }
                }
                // Remove tooltip
                HintTooltip = String.Empty;
                if (_item == null)
                    return;
                
                HintTooltip = TooltipPlaceholder;

                // Set new Icon
                _iconContainer.AddChild(GetIcon(_item));
                _iconContainer.AddChild(ActionMenuProvider.GetActionMenu(_item));
            }
        }
        public override void _Ready() =>
            _iconContainer = GetNode<CenterContainer>(_iconContainerPath);        

        public override bool CanDropData(Vector2 position, object data) =>
            CanDropData(data as CommonItem);

        public virtual bool CanDropData(CommonItem item) =>
            item is CommonItem;

        public override void DropData(Vector2 position, object data) =>
            ItemDropped(data as CommonItem);

        protected virtual void ItemDropped(CommonItem item)
        {
            DuplicationCheck(item);
            SlotItem(item);
            SetItem(item);
        }


        public override object GetDragData(Vector2 position)
        {
            if (Item == null )
                return null;

            DragPreviewParent.SetDragPreview(new DragPreview(GetIcon(Item)));
            var item = Item;            
            ItemDragged();
            return item;
        }

        protected virtual void ItemDragged() =>
            UnslotItem();

        protected void DuplicationCheck(CommonItem item)
        {
            GetParent()
                .GetChildren()
                .Cast<Slot>()
                .FirstOrDefault(x => x.Item == item)
                ?.UnsetItem();
        }

        protected void SetItem(CommonItem item) =>
            Item = item;

        protected void SlotItem(CommonItem item) =>
            item.IsSlotted = true;        

        protected void UnslotItem() =>
            Item.IsSlotted = false;

        public void UnsetItem() =>
            Item = null;

        private CommonIcon GetIcon(CommonItem item) =>
            IconProvider.GetIcon(item);

        public override Control _MakeCustomTooltip(string forText) =>
            TooltipProvider.GetTooltip(Item) as Control;
        
        protected virtual void OnRightClick() { }

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
