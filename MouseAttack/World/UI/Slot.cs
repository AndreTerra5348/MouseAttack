using Godot;
using MouseAttack.Item.ActionMenu;
using MouseAttack.Item.Data;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Provider;
using MouseAttack.Misc;
using MouseAttack.World.UI.Skill;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MouseAttack.World.UI
{
    public abstract class Slot : Button, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        [Export]
        bool SpawnActionMenu { get; set; } = true;

        const string TooltipPlaceholder = "placeholder";

        DragPreviewParent DragPreviewParent => TreeSharer.GetNode<DragPreviewParent>();
        IconProvider IconProvider => TreeSharer.GetNode<IconProvider>();
        TooltipProvider TooltipProvider => TreeSharer.GetNode<TooltipProvider>();
        ActionMenuProvider ActionMenuProvider => TreeSharer.GetNode<ActionMenuProvider>();

        public bool IsEmpty => Item == null;

        [Export]
        NodePath ExtraContainerPath { get; set; }

        CommonIcon _icon = null;
        ItemActionMenu _actionMenu = null;
        CenterContainer _extraContainer = null;

        CommonItem _item = null;
        public CommonItem Item
        {
            get => _item;
            set
            {
                if (_item == value)
                    return;

                // Subscribe to new item values if its not null
                if (value != null)
                    value.PropertyChanged += OnItemPropertyChanged;

                // Unsubscribe from the old item values if its not null
                if (_item != null)
                    _item.PropertyChanged -= OnItemPropertyChanged;

                _item = value;
                OnPropertyChanged();
                OnItemSet();

                // Remove old Icon and ActionMenu
                if (IsInstanceValid(_icon))
                    _icon.QueueFree();
                if (IsInstanceValid(_actionMenu))
                    _actionMenu.QueueFree();

                // Remove tooltip
                HintTooltip = string.Empty;
                if (_item == null)
                    return;

                HintTooltip = TooltipPlaceholder;

                // Set new Icon
                _extraContainer.AddChild(_icon = GetIcon(_item));
                if (SpawnActionMenu)
                    _extraContainer.AddChild(_actionMenu = ActionMenuProvider.GetActionMenu(_item));
            }
        }

        protected virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) { }
        protected virtual void OnItemSet() { }

        public override void _Ready() =>
            _extraContainer = GetNode<CenterContainer>(ExtraContainerPath);

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
            Item = item;
        }

        public override object GetDragData(Vector2 position)
        {
            if (Item == null)
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
