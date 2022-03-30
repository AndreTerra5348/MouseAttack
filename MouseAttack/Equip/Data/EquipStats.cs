using MouseAttack.Characteristic;
using System;

namespace MouseAttack.Equip.Data
{
    public class EquipStats
    {
        public readonly StatsType Type;
        public readonly float Percentage;

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
