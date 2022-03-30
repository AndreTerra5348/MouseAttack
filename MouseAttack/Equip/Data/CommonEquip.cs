using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
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
        [Export]
        public EquipType Type { get; private set; } = EquipType.Weapon;
        public EquipStats PrimaryStats { get; private set; }
        public List<EquipStats> SecondaryStats { get; private set; } = new List<EquipStats>();
        public Color TierColor { get; private set; }        
        public EquipTier Tier { get; private set; } = EquipTier.Common;

        PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();

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

        public override Control GetIcon()
        {
            PanelContainer icon = IconScene.Instance<PanelContainer>();
            StyleBoxFlat styleBox = icon.Get(Overrides.CustomStylesPanel) as StyleBoxFlat;
            styleBox.BorderColor = TierColor;
            return icon;
        }

        public override FloatingLabel GetFloatingDropLabel()
        {
            FloatingLabel floatingLabel = base.GetFloatingDropLabel();
            floatingLabel.Color = TierColor;
            return floatingLabel;
        }
        protected virtual CommonEquip Generate(int monsterLevel)
        {
            Tier = GetRandomTier(monsterLevel);
            PrimaryStats = GetEquipStats(EquipConfig.EquipTypePrimaryStats[Type], monsterLevel);
            
            string tierName = Enum.GetName(typeof(EquipTier), Tier).Capitalize();
            Name = $"{tierName} {Name}";

            TierColor = EquipConfig.EquipTierColor[Tier];

            if (Tier == EquipTier.Common)
                return Duplicate<CommonEquip>();

            int secondaryStatsCount = Tier == EquipTier.Rare ? Random.Next(1, 2) : Random.Next(3, 4);

            EquipStats biggest = new EquipStats(StatsType.None, 0);
            for (int i = 0; i < secondaryStatsCount; i++)
            {
                StatsType secundaryStats = EquipConfig.EquipTypeSecundaryStats[Type].GetRandomElement(Random);
                EquipStats equipStats = GetEquipStats(secundaryStats, monsterLevel);
                if (equipStats.Percentage > biggest.Percentage)
                    biggest = equipStats;
                SecondaryStats.Add(GetEquipStats(secundaryStats, monsterLevel));
            }

            string statsName = Enum.GetName(typeof(StatsType), biggest.Type).Capitalize();

            Name += $" Of {statsName}";

            return Duplicate<CommonEquip>();
        }

        public T Duplicate<T>() where T : Resource =>
            Duplicate(true) as T;

        EquipStats GetEquipStats(StatsType type, int multiplier)
        {
            IntegerRange range = EquipConfig.StatsTypeBasePercentage[type];
            int value = range.GetRandom(Random);
            return new EquipStats(type, value * multiplier);
        }

        public override void ItemDropped(int monsterLevel)
        {            
            CommonEquip equip = Generate(monsterLevel);
            PlayerInventory.Items.Add(equip);
        }

        public EquipTier GetRandomTier(int monsterLevel)
        {
            foreach (var item in EquipConfig.EquipTierDropRate)
            {
                if (item.Value * monsterLevel < Random.Next(100))
                    continue;
                return item.Key;
            }

            return EquipTier.Common;
        }
    }
}
