using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
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
        [Export]
        List<CommonItem> _dropList = new List<CommonItem>();

        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();
            monsterEntity.Dead += (s, e) =>
            {
                foreach(CommonItem item in _dropList)
                {
                    if (!item.Dropped)
                        continue;

                    item.ItemDropped(monsterEntity.Level);
                    FloatingLabel floatingLabel = item.GetFloatingDropLabel();
                    floatingLabel.Position = monsterEntity.Position;
                    monsterEntity.QueueFloatingLabel(floatingLabel);
                }
            };
        }
    }
}
