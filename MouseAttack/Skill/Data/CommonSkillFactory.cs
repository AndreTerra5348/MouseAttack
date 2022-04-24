using Godot;
using MouseAttack.Item.Data;

namespace MouseAttack.Skill.Data
{
    public abstract class CommonSkillFactory : UsableItemFactory
    {
        [AssignTo(nameof(CommonSkill.WorldEffectScene))]
        [Export]
        public PackedScene WorldEffectScene { get; private set; }
        [AssignTo(nameof(CommonSkill.WorldEffectDuration))]
        [Export]
        public int WorldEffectDuration { get; private set; } = 1;
    }
}

