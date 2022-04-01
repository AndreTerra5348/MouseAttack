using Godot;
using MouseAttack.Characteristic;
using System;

namespace MouseAttack.Equip.Data
{
    public class EquipStats
    {
        public StatsType Type { get; private set; }
        public float Percentage { get; private set; }

        public EquipStats(StatsType type, float percentage)
        {
            Type = type;
            Percentage = percentage;
        }

        public override string ToString()
        {
            string typeName = Enum.GetName(typeof(StatsType), Type);
            return $"{typeName} +{Percentage}%";
        }
    }
}
