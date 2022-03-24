using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Item.Drop;
using MouseAttack.Item.Gold;
using MouseAttack.Misc;
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
        int _maxGold;
        [Export]
        int _minGold;

        Random _random = new Random();

        GridController GridController => TreeSharer.GetNode<GridController>();

        public override void _Ready()
        {
            GetParent<MonsterEntity>().Dead += (s, e) =>
            {
                CommonItem gold = new CommonItem();
                if (gold.DropRate > _random.Next(100))
                    return;
                gold.Count = _random.Next(_minGold, _maxGold);
                GoldDrop drop = gold.GetDropInstance<GoldDrop>();
                GridController.AddChild(drop);
            };
        }
    }
}
