using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity.Player.Inventory;
using MouseAttack.Extensions;
using MouseAttack.Item;
using MouseAttack.Item.Data;
using MouseAttack.Item.Icon;
using MouseAttack.Item.Tooltip;
using MouseAttack.Misc;
using MouseAttack.Misc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Equip.Data
{
    public class CommonEquip : CommonItem
    {        
        public EquipType Type { get; private set; }
        public EquipStats PrimaryStats { get; private set; }
        public List<EquipStats> SecondaryStats { get; private set; } = new List<EquipStats>();
        public Color TierColor { get; private set; }        
        public EquipTier Tier { get; private set; } = EquipTier.Common;

        public override string TooltipType => 
            Enum.GetName(typeof(EquipType), Type);
        
        EquipDataChart EquipDataChart => 
            TreeSharer.GetNode<EquipDataChart>();

        public Color GetStatsColor(EquipStats es1, EquipStats es2)
        {
            if (es1.Percentage == es2.Percentage) return EquipStats.NormalColor;
            else if (es1.Percentage > es2.Percentage) 
                return IsSlotted ? EquipStats.LesserColor : EquipStats.GreaterColor;
            return EquipStats.NormalColor;
        }

        public Stack<TooltipInfo> GetTooltipInfo(CommonEquip other)
        {
            if (other == null || other == this)
                return GetTooltipInfo();

            var tooltipInfo = base.GetTooltipInfo();

            var sum = SecondaryStats.GroupBy(
                stats => stats.Type,
                (type, stats) => new EquipStats(type, stats.Sum(s => s.Percentage)));

            var otherSum = other.SecondaryStats.GroupBy(
                stats => stats.Type,
                (type, stats) => new EquipStats(type, stats.Sum(s => s.Percentage)));

            foreach(EquipStats stats in SecondaryStats)
            {
                var myStats = sum.Single(x => x.Type == stats.Type);
                var otherStats = otherSum.SingleOrDefault(x => x.Type == stats.Type);
                Color secondaryColor = Colors.White;
                if (otherStats == null)
                    secondaryColor = IsSlotted ? EquipStats.LesserColor : EquipStats.GreaterColor;
                else 
                    secondaryColor = GetStatsColor(myStats, otherStats);
                tooltipInfo.Push(new TooltipInfo($"{stats}", secondaryColor));
            }

            Color primaryColor = GetStatsColor(PrimaryStats, other.PrimaryStats);
            tooltipInfo.Push(new TooltipInfo($"{PrimaryStats}", primaryColor));

            return tooltipInfo;
        }

        public override Stack<TooltipInfo> GetTooltipInfo()
        {
            var tooltipInfo = base.GetTooltipInfo();            
            foreach (EquipStats equipStats in SecondaryStats)
            {
                tooltipInfo.Push(new TooltipInfo($"{equipStats}", Colors.White));
            }
            tooltipInfo.Push(new TooltipInfo($"{PrimaryStats}", Colors.White));
            return tooltipInfo;
        }

        public void GenerateStats()
        {
            Tier = GetRandomTier();
            string tierName = Enum.GetName(typeof(EquipTier), Tier).Capitalize();
            Name = $"{tierName} {Name}";

            // Set Primary Stats
            PrimaryStats = GetEquipStats(EquipDataChart.EquipTypePrimaryStats[Type]);
            Value += MonsterLevel * (int)PrimaryStats.Percentage;

            // Set Secondary stats
            if (Tier != EquipTier.Common)
                GenerateSecondaryStats(Tier == EquipTier.Rare ? Random.Next(1, 2) : Random.Next(3, 4));

            // Set value
            Value *= MonsterLevel * ((int)Tier + 1) * (SecondaryStats.Count + 1);

            // Update colors
            TierColor = EquipDataChart.EquipTierColor[Tier];
            Color = TierColor;
        }

        void GenerateSecondaryStats(int secondaryStatsCount)
        {
            EquipStats biggest = new EquipStats(StatsType.None, 0);
            for (int i = 0; i < secondaryStatsCount; i++)
            {
                StatsType secundaryStats = EquipDataChart.EquipTypeSecundaryStats[Type].GetRandomElement(Random);
                EquipStats equipStats = GetEquipStats(secundaryStats);
                if (equipStats.Percentage > biggest.Percentage)
                    biggest = equipStats;
                SecondaryStats.Add(equipStats);
                Value += MonsterLevel * (int)equipStats.Percentage;
            }
            SecondaryStats = SecondaryStats.OrderBy(x => x.Type).ToList();
            string statsName = StatsConstants.FullNameMap[biggest.Type];
            Name += $" Of {statsName}";
        }
        EquipStats GetEquipStats(StatsType type)
        {
            IntegerRange range = EquipDataChart.StatsTypeBasePercentage[type].ToIntegerRange();
            int value = range.GetRandom(Random);
            return new EquipStats(type, value * MonsterLevel);
        }

        EquipTier GetRandomTier()
        {
            foreach (var item in EquipDataChart.EquipTierDropRate)
            {
                if (item.Value * MonsterLevel < Random.Next(100))
                    continue;
                return item.Key;
            }

            return EquipTier.Common;
        }        
    }
}
