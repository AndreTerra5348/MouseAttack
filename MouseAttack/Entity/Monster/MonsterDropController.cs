using Godot;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
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
        CommonItem Gold = null;
        [Export]
        int _maxGold = 1;
        [Export]
        int _minGold = 1;

        Random _random = new Random();

        GridController GridController => TreeSharer.GetNode<GridController>();
        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();
            monsterEntity.Dead += (s, e) =>
            {
                if (Gold.DropRate < _random.Next(100))
                    return;
                int count = _random.Next(_minGold, _maxGold);
                Gold.Count += count;
                monsterEntity.QueueFloatingLabel(Gold.GetFloatingDropLabel(monsterEntity.Position, count));           
            };
        }
    }
}
