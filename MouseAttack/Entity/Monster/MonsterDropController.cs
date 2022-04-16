using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Item.Provider;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.Skill.TargetEffect;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class MonsterDropController : Node
    {
        const string ExpTextFormat = "+{0} EXP";
        Color _expTextColor = new Color("96eeaa12");
        DropLabelProvider DropLabelProvider => 
            TreeSharer.GetNode<DropLabelProvider>();
        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();
        FloatingLabelProvider FloatingLabelProvider =>
            TreeSharer.GetNode<FloatingLabelProvider>();

        IEnumerable<CommonItemFactory> GetDropList() =>
            GetChildren().OfType<CommonItemFactory>();

        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();
            IEnumerable<CommonItemFactory> dropList = GetDropList();
            monsterEntity.Dead += (s, e) =>
            {
                FloatingLabel expLabel = FloatingLabelProvider.GetLabel();
                expLabel.Text = String.Format(ExpTextFormat, monsterEntity.Character.Experience);
                expLabel.Position = monsterEntity.Position;
                expLabel.Color = _expTextColor;
                monsterEntity.QueueFloatingLabel(expLabel);

                foreach (CommonItemFactory itemFactory in dropList)
                {
                    if (!itemFactory.Dropped)
                        continue;

                    var item = itemFactory.CreateItem<CommonItem>(monsterEntity.Level);
                    PlayerInventory.Add(item);
                    FloatingLabel floatingLabel = DropLabelProvider.GetDropLabel(item);
                    floatingLabel.Position = monsterEntity.Position;
                    monsterEntity.QueueFloatingLabel(floatingLabel);
                }
            };
        }
    }
}
