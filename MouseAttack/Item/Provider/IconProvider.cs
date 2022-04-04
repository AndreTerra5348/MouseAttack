using Godot;
using MouseAttack.Equip.Data;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Item.Icon;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Provider
{
    public class IconProvider : ItemResourceProvider
    {
        protected override Dictionary<Type, PackedScene> SceneMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(ConsumableItem), null },
        };
        protected override PackedScene DefaultScene => CommonIconScene;

        const float SlotColorAlpha = 0.5f;
        [Export]
        PackedScene CommonIconScene
        {
            get => SceneMap[typeof(CommonItem)];
            set => SceneMap[typeof(CommonItem)] = value;
        }

        [Export]
        PackedScene ConsumableIconScene
        {
            get => SceneMap[typeof(ConsumableItem)];
            set => SceneMap[typeof(ConsumableItem)] = value;
        }        

        CommonIcon GetIcon(CommonItem item, IconColorInfo colorInfo)
        {
            PackedScene iconScene = GetScene(item);
            CommonIcon icon = iconScene.Instance<CommonIcon>();
            icon.Item = item;
            icon.ColorInfo = colorInfo;
            return icon;
        }

        public CommonIcon GetSlotIcon(CommonItem item) =>
            GetIcon(item, new IconColorInfo(item.Color.WithAlpha(SlotColorAlpha), item.Color.WithAlpha(SlotColorAlpha).Contrasted()));

        public CommonIcon GetDropIcon(CommonItem item) =>
            GetIcon(item, new IconColorInfo(Colors.Transparent, Colors.Transparent));
    }
}
