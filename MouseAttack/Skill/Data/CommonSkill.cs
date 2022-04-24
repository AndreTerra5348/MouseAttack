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
    public abstract class CommonSkill : UsableItem
    {
        public event EventHandler Applied;
        const string TypeName = "Skill";

        public PackedScene WorldEffectScene { get; private set; }
        public int WorldEffectDuration { get; private set; } = 1;
        public int ManaCost { get; private set; } = 1;
        public override bool CanUse
        {
            get
            {
                if (ElapsedCooldown > 0)
                    return false;

                if (!PlayArea.OnPlayArea)
                    return false;

                return true;
            }
        }
        public override string TooltipType => TypeName;

        PlayArea PlayArea => TreeSharer.GetNode<PlayArea>();
        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character => PlayerEntity.Character;
        public CommonWorldEffect GetWorldEffect()
        {
            CommonWorldEffect instance = WorldEffectScene.Instance<CommonWorldEffect>();
            instance.Skill = this;
            return instance;
        }

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"World Effect Duration: {WorldEffectDuration}", Colors.White));
            tooltipInfo.Push(new TooltipInfo($"Mana Cost: {ManaCost}", Colors.Aqua));
            return tooltipInfo;
        }

        protected void OnApplied(EventArgs e) =>
            Applied?.Invoke(this, e);

        public override void Use()
        {
            base.Use();

            CommonWorldEffect worldEffect = GetWorldEffect();
            worldEffect.User = PlayerEntity;
            worldEffect.Position = PlayArea.SnappedMousePosition;
            GridController.AddChild(worldEffect);
        }
    }
}

