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
using MouseAttack.Item.Tooltip;
using MouseAttack.Entity.Player;

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
        public bool CanUse
        {
            get
            {
                if (OnCooldown)
                    return false;

                if (!PlayArea.OnPlayArea)
                    return false;

                if (!Character.HasEnoughMana(ManaCost))
                    return false;

                return true;
            }
        }
        public override string TooltipType => "Skill";

        GridController GridController => TreeSharer.GetNode<GridController>();
        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;

        public void StartCooldown() => _cooldown = Cooldown;
        public void ElapseCooldown() => _cooldown--;

        public CommonSkill() =>
            GridController.RoundFinished += OnRoundFinished;

        ~CommonSkill() =>
            GridController.RoundFinished -= OnRoundFinished;

        private void OnRoundFinished(object sender, EventArgs e) =>
            ElapseCooldown();

        public CommonWorldEffect GetWorldEffect()
        {
            CommonWorldEffect instance = WorldEffectScene.Instance<CommonWorldEffect>();
            instance.Skill = this;
            return instance;
        }

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Mana Cost: {ManaCost}", Colors.Aqua));
            tooltipInfo.Push(new TooltipInfo($"Cooldown: {Cooldown}", Colors.Aquamarine));
            return tooltipInfo;
        }

        
        public abstract void Apply(CommonEntity user, CommonEntity target);

        protected void OnApplied(EventArgs e) =>
            Applied?.Invoke(this, e);

        public void Use()
        {
            Character.UseMana(ManaCost);

            StartCooldown();

            CommonWorldEffect worldEffect = GetWorldEffect();
            worldEffect.User = PlayerEntity;
            worldEffect.Position = PlayArea.SnappedMousePosition;
            GridController.AddChild(worldEffect);
        }
    }
}

