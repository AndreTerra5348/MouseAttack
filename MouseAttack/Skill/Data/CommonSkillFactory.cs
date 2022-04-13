using Godot;
using MouseAttack.Item.Data;

namespace MouseAttack.Skill.Data
{
    public abstract class CommonSkillFactory : UsableItemFactory
    {
        [AssignTo(nameof(CommonSkill.WorldEffectScene))]
        [Export]
        public PackedScene WorldEffectScene { get; private set; }
        [AssignTo(nameof(CommonSkill.ManaCost))]
        [Export]
        public int ManaCost { get; private set; } = 1;    
    }
}

