using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Equip.Data
{
    public enum EquipTier
    {
        Common,
        Rare,
        Epic
    }

    public enum EquipType
    {
        Offensive,
        Defensive,
        Special
    }
    public class EquipDataChart : Node, ISharable
    {
        #region Map
        public Dictionary<EquipTier, int> EquipTierDropRate { get; private set; } = new Dictionary<EquipTier, int>()
        {
            { EquipTier.Rare, 10 },
            { EquipTier.Epic, 5 },
        };
        public Dictionary<EquipType, int> EquipTypeDropRate { get; private set; } = new Dictionary<EquipType, int>()
        {
            { EquipType.Offensive, 50 },
            { EquipType.Defensive, 25 },
            { EquipType.Special, 5 },
        };

        public Dictionary<StatsType, Vector2> StatsTypeBasePercentage { get; private set; } = new Dictionary<StatsType, Vector2>()
        {
            { StatsType.Damage, new Vector2(1 ,3) },
            { StatsType.Defense, new Vector2(1 ,3) },
            { StatsType.CriticalDamage, new Vector2(1 ,3) },
            { StatsType.CriticalRate, new Vector2(1 ,3) },
            { StatsType.Health, new Vector2(1 ,3) },
            { StatsType.HealthRegen, new Vector2(1 ,3) },
            { StatsType.Mana, new Vector2(1 ,3) },
            { StatsType.ManaRegen, new Vector2(1 ,3) },
        };

        public Dictionary<EquipTier, Color> EquipTierColor { get; private set; } = new Dictionary<EquipTier, Color>()
        {
            { EquipTier.Common, Colors.DarkGray },
            { EquipTier.Rare, Colors.DarkCyan },
            { EquipTier.Epic, Colors.DarkViolet },

        };

        public Dictionary<EquipType, StatsType> EquipTypePrimaryStats { get; private set; } = new Dictionary<EquipType, StatsType>()
        {
            { EquipType.Offensive,  StatsType.Damage },
            { EquipType.Defensive,  StatsType.Defense },
            { EquipType.Special,  StatsType.Health },
        };

        public Dictionary<EquipType, List<StatsType>> EquipTypeSecundaryStats { get; private set; } = new Dictionary<EquipType, List<StatsType>>()
        {
            {
                EquipType.Offensive,
                new List<StatsType>()
                {
                    StatsType.CriticalDamage,
                    StatsType.CriticalRate
                }
            },
            {
                EquipType.Defensive,
                new List<StatsType>()
                {
                    StatsType.Health,
                    StatsType.HealthRegen

                }
            },
            {
                EquipType.Special,
                new List<StatsType>()
                {
                    StatsType.Damage,
                    StatsType.Defense,
                    StatsType.HealthRegen,
                    StatsType.CriticalDamage,
                    StatsType.CriticalRate
                }
            },
        };
        #endregion

        #region Tier Drop
        [Export]
        public int RareDropRate 
        {
            get => EquipTierDropRate[EquipTier.Rare];
            private set => EquipTierDropRate[EquipTier.Rare] = value;
        }
        [Export]
        public int EpicDropRate 
        {
            get => EquipTierDropRate[EquipTier.Epic];
            private set => EquipTierDropRate[EquipTier.Epic] = value;
        }
        #endregion

        #region Equip Type Drop
        [Export]
        public int OffensiveDropRate 
        {
            get => EquipTypeDropRate[EquipType.Offensive];
            private set => EquipTypeDropRate[EquipType.Offensive] = value;
        }
        [Export]
        public int DefensiveDropRate
        {
            get => EquipTypeDropRate[EquipType.Defensive];
            private set => EquipTypeDropRate[EquipType.Defensive] = value;
        }
        [Export]
        public int SpecialDropRate
        {
            get => EquipTypeDropRate[EquipType.Special];
            private set => EquipTypeDropRate[EquipType.Special] = value;
        }
        #endregion

        #region Base Percentage
        [Export]
        public Vector2 Damage
        {
            get => StatsTypeBasePercentage[StatsType.Damage];
            private set => StatsTypeBasePercentage[StatsType.Damage] = value;
        }
        [Export]
        public Vector2 Defense
        {
            get => StatsTypeBasePercentage[StatsType.Defense];
            private set => StatsTypeBasePercentage[StatsType.Defense] = value;
        }
        [Export]
        public Vector2 CriticalDamage
        {
            get => StatsTypeBasePercentage[StatsType.CriticalDamage];
            private set => StatsTypeBasePercentage[StatsType.CriticalDamage] = value;
        }
        [Export]
        public Vector2 CriticalRate
        {
            get => StatsTypeBasePercentage[StatsType.CriticalRate];
            private set => StatsTypeBasePercentage[StatsType.CriticalRate] = value;
        }
        [Export]
        public Vector2 Health
        {
            get => StatsTypeBasePercentage[StatsType.Health];
            private set => StatsTypeBasePercentage[StatsType.Health] = value;
        }
        [Export]
        public Vector2 HealthRegen
        {
            get => StatsTypeBasePercentage[StatsType.HealthRegen];
            private set => StatsTypeBasePercentage[StatsType.HealthRegen] = value;
        }
        [Export]
        public Vector2 Mana
        {
            get => StatsTypeBasePercentage[StatsType.Mana];
            private set => StatsTypeBasePercentage[StatsType.Mana] = value;
        }
        [Export]
        public Vector2 ManaRegen
        {
            get => StatsTypeBasePercentage[StatsType.ManaRegen];
            private set => StatsTypeBasePercentage[StatsType.ManaRegen] = value;
        }
        #endregion

        #region Tier Color
        [Export]
        public Color CommonColor
        {
            get => EquipTierColor[EquipTier.Common];
            private set => EquipTierColor[EquipTier.Common] = value;
        }
        [Export]
        public Color RareColor
        {
            get => EquipTierColor[EquipTier.Rare];
            private set => EquipTierColor[EquipTier.Rare] = value;
        }
        [Export]
        public Color EpicColor
        {
            get => EquipTierColor[EquipTier.Epic];
            private set => EquipTierColor[EquipTier.Epic] = value;
        }
        #endregion

        #region Type Stats
        [Export]
        public StatsType OffensivePrimaryStats
        {
            get => EquipTypePrimaryStats[EquipType.Offensive];
            private set => EquipTypePrimaryStats[EquipType.Offensive] = value;
        }
        [Export]
        public List<StatsType> OffensiveSecundaryStats
        {
            get => EquipTypeSecundaryStats[EquipType.Offensive];
            private set => EquipTypeSecundaryStats[EquipType.Offensive] = value;
        }
        [Export]
        public StatsType DefensivePrimaryStats
        {
            get => EquipTypePrimaryStats[EquipType.Defensive];
            private set => EquipTypePrimaryStats[EquipType.Defensive] = value;
        }
        [Export]
        public List<StatsType> DefensiveSecundaryStats
        {
            get => EquipTypeSecundaryStats[EquipType.Defensive];
            private set => EquipTypeSecundaryStats[EquipType.Defensive] = value;
        }
        [Export]
        public StatsType SepcialPrimaryStats
        {
            get => EquipTypePrimaryStats[EquipType.Special];
            private set => EquipTypePrimaryStats[EquipType.Special] = value;
        }
        [Export]
        public List<StatsType> SepcialSecundaryStats
        {
            get => EquipTypeSecundaryStats[EquipType.Special];
            private set => EquipTypeSecundaryStats[EquipType.Special] = value;
        }
        #endregion

        public EquipDataChart() =>
            TreeSharer.RegistryNode(this);
    }
}
