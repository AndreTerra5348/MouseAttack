using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using MouseAttack.Skill.TargetEffect;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public class CooldownEventArgs : EventArgs
    {
        public readonly int Cooldown;

        public CooldownEventArgs(int cooldown) =>
            Cooldown = cooldown;
    }
    public abstract class UsableItem : CommonItem
    {
        public event EventHandler<CooldownEventArgs> CooldownStarted;
        public int Cooldown { get; private set; }
        public Color EffectColor { get; private set; }
        public int Duration { get; private set; } = 0;
        protected int ElapsedDuration { get; set; } = 0;
        public int ElapsedCooldown { get; protected set; } = 0;
        public virtual bool CanUse { get; }
        public List<TargetEffectSpawner> TargetEffectSpawners { get; private set; }

        protected FloatingLabelProvider FloatingLabelProvider => 
            TreeSharer.GetNode<FloatingLabelProvider>();

        protected virtual Action ApplyAction { get; set; }

        protected override void OnRoundFinished()
        {
            ElapsedCooldown--;
            base.OnRoundFinished();
            if (ElapsedDuration > 0)
            {
                ElapsedDuration--;
                ApplyAction();
            }                
        }

        protected void SpawnFloatingLabel(CommonEntity target, string text)
        {
            var label = FloatingLabelProvider.GetLabel();
            label.Text = text;
            label.Color = EffectColor;
            label.Position = target.GlobalPosition;
            target.QueueFloatingLabel(label);
        }

        protected void SpawnTargetEffects(CommonEntity target)
        {
            foreach (var spawner in TargetEffectSpawners)
            {
                spawner.Spawn(target);
            }
        } 
        
        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Duration: {Duration}", Colors.MediumVioletRed));
            tooltipInfo.Push(new TooltipInfo($"Cooldown: {Cooldown}", Colors.Aquamarine));
            return tooltipInfo;
        }

        public virtual void SlotInputEvent(InputEvent @event) { }
        public virtual void SlotPressed() { }
        public virtual void Use()
        {
            ElapsedDuration = Duration;
            ElapsedCooldown = Cooldown;
            CooldownStarted?.Invoke(this, new CooldownEventArgs(Cooldown));
            GridController.ElapseTurn();
        }
    }
}
