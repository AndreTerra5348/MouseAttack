using Godot;
using MouseAttack.Skill.TargetEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public abstract class UsableItemFactory : CommonItemFactory
    {
        [AssignTo(nameof(UsableItem.Cooldown))]
        [Export]
        public int Cooldown { get; private set; } = 1;
        [AssignTo(nameof(UsableItem.EffectDuration))]
        [Export]
        public int EffectDuration { get; private set; } = 0;
        [AssignTo(nameof(UsableItem.EffectColor))]
        [Export]
        public Color EffectColor { get; private set; } = Colors.White;
        [AssignTo(nameof(UsableItem.TargetEffectSpawners))]
        [Export]
        public List<TargetEffectSpawner> TargetEffectSpawners { get; private set; }
    }
}
