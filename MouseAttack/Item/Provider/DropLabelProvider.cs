using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Provider
{
    public class DropLabelProvider : ItemResourceProvider
    {
        protected override Dictionary<Type, PackedScene> SceneMap { get; set; } = new Dictionary<Type, PackedScene>()
        {
            { typeof(CommonItem), null },
        };
        protected override PackedScene DefaultScene => FloatingLabelScene;
        IconProvider IconProvider => TreeSharer.GetNode<IconProvider>();

        [Export]
        PackedScene FloatingLabelScene
        {
            get => SceneMap[typeof(CommonItem)];
            set => SceneMap[typeof(CommonItem)] = value;
        }

        public FloatingLabel GetDropLabel(CommonItem item)
        {
            PackedScene dropLabelScene = GetScene(item);
            FloatingLabel floatingLabel = dropLabelScene.Instance<FloatingLabel>();
            floatingLabel.Icon = IconProvider.GetDropIcon(item);
            floatingLabel.Text = item.DropText;
            floatingLabel.Color = new Color(item.Color.r, item.Color.g, item.Color.b, 1.0f);
            return floatingLabel;
        }

        
    }
}
