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
        int _maxGold = 1;
        [Export]
        int _minGold = 1;

        Random _random = new Random();

        CommonItem PlayerGold => TreeSharer.GetNode<PlayerInventory>().Gold;

        public override void _Ready()
        {
            MonsterEntity monsterEntity = GetParent<MonsterEntity>();
            monsterEntity.Dead += (s, e) =>
            {                
                if (PlayerGold.DropRate < _random.Next(100))
                    return;
                int count = _random.Next(_minGold, _maxGold);
                PlayerGold.Count += count;
                FloatingLabel floatingLabel = PlayerGold.GetFloatingDropLabel();
                floatingLabel.Position = monsterEntity.Position;
                floatingLabel.Text = count.ToString();
                monsterEntity.QueueFloatingLabel(floatingLabel);           
            };
        }
    }
}
