using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action.Monster
{
    public class MonsterDamageAction : DamageAction
    {
        [Export]
        public float Range { get; private set; }
    }
}
