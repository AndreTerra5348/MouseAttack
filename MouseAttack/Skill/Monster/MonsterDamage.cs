using Godot;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.Monster
{
    public class MonsterDamage : DamageSkill
    {
        [Export]
        public int Range { get; private set; }
    }
}
