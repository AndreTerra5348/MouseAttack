using Godot;
using MouseAttack.Skill.WorldEffect;
using MouseAttack.Characteristic;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System.Collections.Generic;
using System;
using MouseAttack.Skill.TargetEffect;

namespace MouseAttack.Skill.Data
{
    public class CommonSkillInfo
    {
        public readonly CommonEntity Target;

        public CommonSkillInfo(CommonEntity target) => 
            Target = target;
    }
    /// <summary>
    /// Base class for all Skills
    /// </summary>
    public abstract class CommonSkill : Resource
    {
        [Export]
        public PackedScene WorldEffectScene { get; private set; }
        [Export]
        public List<CommonTargetEffectSpawner> TargetEffectSpaners { get; private set; }
        [Export]
        public PackedScene ItemScene { get; private set; }
        [Export]
        public Texture Icon { get; private set; }
        [Export]
        public int Cost { get; private set; } = 1;
        [Export]
        public int Duration { get; private set; } = 1;
        [Export]
        public int Cooldown { get; private set; } = 1;
        [Export]
        public bool IsUnlocked { get; private set; } = false;

        public event EventHandler SkillUsed;

        int _cooldown = 0;
        public virtual string Tooltip
        {
            get
            {
                return
                    $"Cost: {Cost}\n" +
                    $"Cooldown: {Cooldown}";
            }
        }
        public bool OnCooldown => _cooldown > 0;

        public CommonWorldEffect GetWorldEffectInstance() => WorldEffectScene.Instance<CommonWorldEffect>();
        public void StartCooldown() => _cooldown = Cooldown;
        public void ElapseCooldown() => _cooldown--;
        public abstract void Use(CommonEntity user, CommonEntity target);

        protected void OnSkillUsed(EventArgs e) =>
            SkillUsed?.Invoke(this, e);


    }
}

