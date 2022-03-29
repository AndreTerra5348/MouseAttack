using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Equip.Data
{
    public class CommonEquip : CommonItem
    {
        [Export]
        public EquipType Type { get; private set; } = EquipType.Weapon;
        public EquipStats PrimaryStats { get; private set; }
        public List<EquipStats> SecondaryStats { get; private set; }
        public Color TierColor { get; private set; }        
        public EquipTier Tier { get; private set; } = EquipTier.Common;
        Random _random = new Random();
        public virtual CommonEquip Generate(int multiplier = 1)
        {
            PrimaryStats = GetEquipStats(EquipConfig.EquipTypePrimaryStats[Type], multiplier);

            string tierName = Enum.GetName(typeof(EquipTier), Tier).Capitalize();
            Name = $"{tierName} {Name}";

            TierColor = EquipConfig.EquipTierColor[Tier];

            if (Tier == EquipTier.Common)
                return Duplicate<CommonEquip>();

            int secondaryStatsCount = Tier == EquipTier.Rare ? _random.Next(1, 2) : _random.Next(3, 4);

            EquipStats biggest = new EquipStats(StatsType.None, 0);
            for (int i = 0; i < secondaryStatsCount; i++)
            {
                StatsType secundaryStats = EquipConfig.EquipTypeSecundaryStats[Type].GetRandomElement(_random);
                EquipStats equipStats = GetEquipStats(secundaryStats, multiplier);
                if (equipStats.Percentage > biggest.Percentage)
                    biggest = equipStats;
                SecondaryStats.Add(GetEquipStats(secundaryStats, multiplier));
            }

            string statsName = Enum.GetName(typeof(StatsType), biggest.Type).Capitalize();

            Name += $" Of {statsName}";

            return Duplicate<CommonEquip>();
        }

        public T Duplicate<T>() where T : Resource =>
            base.Duplicate() as T;

        EquipStats GetEquipStats(StatsType type, int multiplier)
        {
            IntegerRange range = EquipConfig.StatsTypeBasePercentage[type];
            int value = range.GetRandom(_random);
            return new EquipStats(type, value * multiplier);
        }
    }
}
