using Godot;
using MouseAttack.Entity.Monster;
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

        Random _random = new Random();
        public MonsterBody GetRandomMonster() => Monsters[_random.Next(Monsters.Count)].Instance<MonsterBody>();
    }
}
