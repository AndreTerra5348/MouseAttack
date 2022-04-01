using Godot;
using MouseAttack.Entity.Monster;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.Monster
{
    public class MonsterPool : Resource
    {
        [Export]
        public List<PackedScene> Monsters { get; private set; }
        Stage Stage => TreeSharer.GetNode<Stage>();
        public MonsterEntity GetRandomMonster() => Monsters[Stage.Random.Next(Monsters.Count)].Instance<MonsterEntity>();
    }
}
