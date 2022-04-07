using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Item.Data;
using MouseAttack.Item.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Provider
{
    public class TooltipProvider : ItemResourceProvider<PackedScene>
    {
        protected override Dictionary<Type, PackedScene> ResourceMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(CommonEquip), null },
        };

        protected override PackedScene DefaultResource => ItemTooltipScene;

        [Export]
        PackedScene ItemTooltipScene
        {
            get => ResourceMap[typeof(CommonItem)];
            set => ResourceMap[typeof(CommonItem)] = value;
        }
        [Export]
        PackedScene EquipTooltipScene
        {
            get => ResourceMap[typeof(CommonEquip)];
            set => ResourceMap[typeof(CommonEquip)] = value;
        }

        public IItemView GetTooltip(CommonItem item)
        {
            PackedScene tooltipScene = GetResource(item);
            IItemView tooltipPanel = tooltipScene.Instance<IItemView>();
            tooltipPanel.SetItem(item);
            return tooltipPanel;
        }
    }
}
