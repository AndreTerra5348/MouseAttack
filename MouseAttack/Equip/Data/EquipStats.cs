using MouseAttack.Characteristic;

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
    }
}
