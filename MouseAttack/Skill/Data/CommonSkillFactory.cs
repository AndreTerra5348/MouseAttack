using Godot;
using MouseAttack.Item.Data;

namespace MouseAttack.Skill.Data
{
    public abstract class CommonSkillFactory : CommonItemFactory
    {
        [AssignTo(nameof(CommonSkill.WorldEffectScene))]
        [Export]
        public PackedScene WorldEffectScene { get; private set; }
        [AssignTo(nameof(CommonSkill.ManaCost))]
        [Export]
        public int ManaCost { get; private set; } = 1;
        [AssignTo(nameof(CommonSkill.Duration))]
        [Export]
        public int Duration { get; private set; } = 0;
        [AssignTo(nameof(CommonSkill.Cooldown))]
        [Export]
        public int Cooldown { get; private set; } = 1;
    }
}

