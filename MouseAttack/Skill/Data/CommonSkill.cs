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

    /// <summary>
    /// Base class for all Skills
    /// </summary>
    public abstract class CommonSkill : CommonItem
    {
        public event EventHandler Applied;
        public PackedScene WorldEffectScene { get; private set; }
        public int ManaCost { get; private set; } = 1;
        public int Duration { get; private set; } = 0;
        public int Cooldown { get; private set; } = 1;

        int _cooldown = 0;
        public bool OnCooldown => _cooldown > 0;
        public override string Tooltip =>
            $"Mana Cost: {ManaCost}\nCooldown: {Cooldown}\n{base.Tooltip}";


        public CommonWorldEffect GetWorldEffect()
        {
            CommonWorldEffect instance = WorldEffectScene.Instance<CommonWorldEffect>();
            instance.Skill = this;
            return instance;
        }
            
        public void StartCooldown() => _cooldown = Cooldown;
        public void ElapseCooldown() => _cooldown--;
        public abstract void Apply(CommonEntity user, CommonEntity target);
        protected void OnApplied(EventArgs e) =>
            Applied?.Invoke(this, e);
    }
}

