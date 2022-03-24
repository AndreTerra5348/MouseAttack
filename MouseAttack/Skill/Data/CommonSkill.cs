using Godot;
using MouseAttack.Skill.WorldEffect;
using MouseAttack.Characteristic;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.World;
using System.Collections.Generic;
using System;
using MouseAttack.Skill.TargetEffect;
using MouseAttack.Item.Data;

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
    public abstract class CommonSkill : CommonItem, ISellable
    {
        [Export]
        PackedScene WorldEffectScene { get; set; }
        /// <summary>
        /// Instantiate World Effect
        /// </summary>
        public CommonWorldEffect NewWorldEffect => WorldEffectScene.Instance<CommonWorldEffect>();       
        [Export]
        protected List<CommonTargetEffectSpawner> TargetEffectSpaners { get; private set; }        
        [Export]
        public int Price { get; private set; } = 1;
        [Export]
        public int ManaCost { get; private set; } = 1;
        [Export]
        public int Duration { get; private set; } = 1;
        [Export]
        public int Cooldown { get; private set; } = 1;
        [Export]
        public bool IsUnlocked { get; private set; } = false;

        int _cooldown = 0;
        public override string Tooltip
        {
            get
            {
                return
                    $"Mana Cost: {ManaCost}\n" +
                    $"Cooldown: {Cooldown}";
            }
        }
        public bool OnCooldown => _cooldown > 0;

        public CommonWorldEffect GetWorldEffectInstance() => WorldEffectScene.Instance<CommonWorldEffect>();
        public void StartCooldown() => _cooldown = Cooldown;
        public void ElapseCooldown() => _cooldown--;
        public abstract void Use(CommonEntity user, CommonEntity target);


    }
}

