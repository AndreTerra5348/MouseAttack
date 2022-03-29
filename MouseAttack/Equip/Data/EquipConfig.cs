using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Misc;
using System.Collections.Generic;

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
        Weapon,
        Armor,
        Jewelry
    }

    public class EquipConfig
    {
        public static Dictionary<EquipTier, int> TierDropRate { get; private set; } = new Dictionary<EquipTier, int>()
        {
            { EquipTier.Common, 50 },
            { EquipTier.Rare, 30 },
            { EquipTier.Epic, 10 },
        };
        public static Dictionary<EquipType, int> EquipTypeDropRate { get; private set; } = new Dictionary<EquipType, int>()
        {
            { EquipType.Weapon, 50 },
            { EquipType.Armor, 30 },
            { EquipType.Jewelry, 10 },
        };

        public static Dictionary<StatsType, IntegerRange> StatsTypeBasePercentage { get; private set; }  = new Dictionary<StatsType, IntegerRange>()
        {
            { StatsType.Damage, new IntegerRange(1 ,3) },
            { StatsType.Defense, new IntegerRange(1 ,3) },
            { StatsType.CriticalDamage, new IntegerRange(1 ,3) },
            { StatsType.CriticalRate, new IntegerRange(1 ,3) },
            { StatsType.Health, new IntegerRange(1 ,3) },
            { StatsType.HealthRegen, new IntegerRange(1 ,3) },
            { StatsType.Mana, new IntegerRange(1 ,3) },
            { StatsType.ManaRegen, new IntegerRange(1 ,3) },
        };

        public static Dictionary<EquipTier, Color> EquipTierColor { get; private set; } = new Dictionary<EquipTier, Color>()
        {
            { EquipTier.Common, Colors.DarkGray },
            { EquipTier.Rare, Colors.DarkCyan },
            { EquipTier.Epic, Colors.DarkViolet },

        };

        public static Dictionary<EquipType, StatsType> EquipTypePrimaryStats { get; private set; } = new Dictionary<EquipType, StatsType>()
        {
            { EquipType.Weapon,  StatsType.Damage },
            { EquipType.Armor,  StatsType.Defense },
            { EquipType.Jewelry,  StatsType.Defense },
        };

        public static Dictionary<EquipType, List<StatsType>> EquipTypeSecundaryStats { get; private set; } = new Dictionary<EquipType, List<StatsType>>()
        {
            { 
                EquipType.Weapon,  
                new List<StatsType>() 
                { 
                    StatsType.CriticalDamage,
                    StatsType.CriticalRate 
                } 
            },
            { 
                EquipType.Armor,  
                new List<StatsType>() 
                { 
                    StatsType.Health, 
                    StatsType.Mana 
                } 
            },
            { 
                EquipType.Jewelry,  
                new List<StatsType>() 
                { 
                    StatsType.ManaRegen, 
                    StatsType.HealthRegen, 
                    StatsType.Health, 
                    StatsType.Mana, 
                    StatsType.CriticalDamage, 
                    StatsType.CriticalRate 
                } 
            },
        };
    }
}
