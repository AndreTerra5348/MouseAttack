using Godot;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill
{
    public class SkillDB : Resource
    {
        [Export]
        public List<CommonSkill> Skills { get; private set; }

        public List<CommonSkill> GetUnlockedSkills() => Skills.Where(a => a.IsUnlocked).ToList();

        public CommonSkill GetMainAttack() => Skills.First();
    }
}
