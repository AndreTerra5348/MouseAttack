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
    public class TooltipProvider : ItemResourceProvider
    {
        protected override Dictionary<Type, PackedScene> SceneMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(CommonEquip), null },
        };
        protected override PackedScene DefaultScene => ItemTooltipScene;

        [Export]
        PackedScene ItemTooltipScene
        {
            get => SceneMap[typeof(CommonItem)];
            set => SceneMap[typeof(CommonItem)] = value;
        }
        [Export]
        PackedScene EquipTooltipScene
        {
            get => SceneMap[typeof(CommonEquip)];
            set => SceneMap[typeof(CommonEquip)] = value;
        }

        public ITooltipPanel GetTooltip(CommonItem item)
        {
            PackedScene tooltipScene = GetScene(item);
            ITooltipPanel tooltipPanel = tooltipScene.Instance<ITooltipPanel>();
            tooltipPanel.SetItem(item);
            return tooltipPanel;
        }

        Stack<TooltipInfo> BuildTooltipInfo(CommonItem item)
        {
            Stack<TooltipInfo> info = new Stack<TooltipInfo>();

            if (item is CommonItem)
                info.Push(new TooltipInfo($"Value: {item.Value}", Colors.Yellow));           

            return info;
        }


    }
}
