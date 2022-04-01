using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Item;
using MouseAttack.Item.Data;
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

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();
        EquipDataChart EquipDataChart => TreeSharer.GetNode<EquipDataChart>();

        public override string Tooltip
        {
            get
            {
                string toolTip = $"{PrimaryStats.ToString()}\n";

                foreach (EquipStats equipStats in SecondaryStats)
                {
                    toolTip += $"{equipStats.ToString()}\n";
                }
                return $"{toolTip}{base.Tooltip}";
            }
        }        

        public override void ItemDropped(int monsterLevel)
        {
            Tier = GetRandomTier(monsterLevel);
            PrimaryStats = GetEquipStats(EquipDataChart.EquipTypePrimaryStats[Type], monsterLevel);

            string tierName = Enum.GetName(typeof(EquipTier), Tier).Capitalize();
            Name = $"{tierName} {Name}";

            TierColor = EquipDataChart.EquipTierColor[Tier];

            if (Tier != EquipTier.Common)
                GenerateSecundaryStats(monsterLevel, Tier == EquipTier.Rare ? Random.Next(1, 2) : Random.Next(3, 4));

            PlayerInventory.Items.Add(this);
        }
        private void GenerateSecundaryStats(int monsterLevel, int secondaryStatsCount)
        {
            EquipStats biggest = new EquipStats(StatsType.None, 0);
            for (int i = 0; i < secondaryStatsCount; i++)
            {
                StatsType secundaryStats = EquipDataChart.EquipTypeSecundaryStats[Type].GetRandomElement(Random);
                EquipStats equipStats = GetEquipStats(secundaryStats, monsterLevel);
                if (equipStats.Percentage > biggest.Percentage)
                    biggest = equipStats;
                SecondaryStats.Add(GetEquipStats(secundaryStats, monsterLevel));
            }

            string statsName = Enum.GetName(typeof(StatsType), biggest.Type).Capitalize();

            Name += $" Of {statsName}";
        }
        EquipStats GetEquipStats(StatsType type, int multiplier)
        {
            IntegerRange range = EquipDataChart.StatsTypeBasePercentage[type].ToIntegerRange();
            int value = range.GetRandom(Random);
            return new EquipStats(type, value * multiplier);
        }

        public EquipTier GetRandomTier(int monsterLevel)
        {
            foreach (var item in EquipDataChart.EquipTierDropRate)
            {
                if (item.Value * monsterLevel < Random.Next(100))
                    continue;
                return item.Key;
            }

            return EquipTier.Common;
        }


        public override Icon GetSlotIcon()
        {
            Icon icon = base.GetSlotIcon();
            icon.BorderColor = TierColor;
            icon.BgColor = TierColor.Contrasted();
            return icon;
        }

        public override FloatingLabel GetFloatingDropLabel()
        {
            FloatingLabel floatingLabel = base.GetFloatingDropLabel();
            floatingLabel.Color = new Color(TierColor.r, TierColor.g, TierColor.b, 1.0f);
            return floatingLabel;
        }

        
    }
}
