using MouseAttack.Characteristic;
using MouseAttack.Entity.Player;
using MouseAttack.Item.Data;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.World.UI.Buff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Consumable
{
    public class StatsPotion : ConsumableItem
    {
        public StatsType Type { get; private set; }

        float _percentage = 0;
        public float Percentage
        {
            get => _percentage * MonsterLevel;
            private set => _percentage = value;
        }
        protected override bool ApplyEveryTurn => false;
        string TypeName => StatsConstants.FullNameMap[Type];
        string TypeShortName => StatsConstants.NameMap[Type];

        string _name = "";
        public override string Name
        {
            get => $"{TypeName} Potion Lv. {MonsterLevel}";
            protected set => _name = value;
        }

        public override string UseText => $"+{Percentage}% {TypeShortName}";


        PlayerEntity PlayerEntity =>
            TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter Character =>
            PlayerEntity.Character;

        BuffDisplay BuffDisplay => TreeSharer.GetNode<BuffDisplay>();

        public override string DropText => $"{Count} {Name}";

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            Stack<TooltipInfo> tooltipInfo = base.GetTooltipInfo();
            tooltipInfo.Push(new TooltipInfo($"Increase: {TypeName} {Percentage}%", Color));
            return tooltipInfo;
        }

        public override void Use()
        {
            base.Use();

            Stats stats = Character.GetStats(Type);
            stats.AlteredPercentage += Percentage;
            BuffDisplay.AddBuff(UseText, EffectDuration);
        }

        protected override void OnDurationFinished()
        {
            base.OnDurationFinished();

            Stats stats = Character.GetStats(Type);
            stats.AlteredPercentage -= Percentage;
        }
    }
}
