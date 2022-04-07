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
    public class IconProvider : ItemResourceProvider<PackedScene>
    {
        protected override Dictionary<Type, PackedScene> ResourceMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
            { typeof(ConsumableItem), null },
        };

        protected override PackedScene DefaultResource => CommonIconScene;
        
        [Export]
        PackedScene CommonIconScene
        {
            get => ResourceMap[typeof(CommonItem)];
            set => ResourceMap[typeof(CommonItem)] = value;
        }
        [Export]
        PackedScene ConsumableIconScene
        {
            get => ResourceMap[typeof(ConsumableItem)];
            set => ResourceMap[typeof(ConsumableItem)] = value;
        }

        public CommonIcon GetIcon(CommonItem item)
        {
            PackedScene iconScene = GetResource(item);
            CommonIcon icon = iconScene.Instance<CommonIcon>();
            icon.SetItem(item);
            return icon;
        }
    }
}
