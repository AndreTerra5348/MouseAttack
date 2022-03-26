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
    public abstract class CommonSkill : CommonItem, ISellable
    {
        public event EventHandler Applied;
        [Export]
        PackedScene WorldEffectScene { get; set; }
        [Export]
        List<TargetEffectSpawner> TargetEffectSpawners { get; set; } = new List<TargetEffectSpawner>();
        [Export]
        public int Price { get; private set; } = 1;
        [Export]
        public int ManaCost { get; private set; } = 1;
        [Export]
        public int Duration { get; private set; } = 0;
        [Export]
        public int Cooldown { get; private set; } = 1;

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

        public CommonWorldEffect GetWorldEffect(CommonEntity user, Vector2 globalPosition)
        {
            CommonWorldEffect instance = WorldEffectScene.Instance<CommonWorldEffect>();
            instance.Skill = this;
            instance.User = user;
            instance.GlobalPosition = globalPosition;
            return instance;
        }
            
        public void StartCooldown() => _cooldown = Cooldown;
        public void ElapseCooldown() => _cooldown--;
        public abstract void Apply(CommonEntity user, CommonEntity target);
        protected void OnApplied(EventArgs e) =>
            Applied?.Invoke(this, e);
        protected void SpawnTargetEffects(CommonEntity target) =>
            TargetEffectSpawners.ForEach(spawner => spawner.Spawn(target));
    }
}

