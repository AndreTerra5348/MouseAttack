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
        IEnumerable<CommonItemFactory> GetDropList() =>
            GetChildren().OfType<CommonItemFactory>();

        DropLabelProvider DropLabelProvider => 
            TreeSharer.GetNode<DropLabelProvider>();
        PlayerInventory PlayerInventory =>
            TreeSharer.GetNode<PlayerInventory>();

        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();
            IEnumerable<CommonItemFactory> dropList = GetDropList();
            monsterEntity.Dead += (s, e) =>
            {
                foreach (CommonItemFactory itemFactory in dropList)
                {
                    if (!itemFactory.Dropped)
                        continue;

                    var item = itemFactory.CreateItem<CommonItem>();
                    item.ItemDropped(monsterEntity.Level);
                    if (item.IsStorable)
                        PlayerInventory.Add(item);
                    FloatingLabel floatingLabel = DropLabelProvider.GetDropLabel(item);
                    floatingLabel.Position = monsterEntity.Position;
                    monsterEntity.QueueFloatingLabel(floatingLabel);
                }
            };
        }
    }
}
