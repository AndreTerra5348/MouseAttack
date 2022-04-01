using Godot;
using MouseAttack.Item.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.Data
{
    public class DamageSkillFactory : CollidableSkillFactory
    {
        [AssignTo(nameof(DamageSkill.Damage))]
        [Export]
        public int Damage { get; private set; } = 1;
        [AssignTo(nameof(DamageSkill.Hits))]
        [Export]
        public int Hits { get; private set; } = 1;

        protected override CommonItem GetNewItem() =>
            new DamageSkill();
    }
}
