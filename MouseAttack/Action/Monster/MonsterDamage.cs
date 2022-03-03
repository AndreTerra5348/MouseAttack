using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action.Monster
{
    public class MonsterDamage : DamageAction, IMonsterAction
    {
        [Export]
        public float Speed { get; private set; }
        [Export]
        public float Range { get; private set; }
    }
}
