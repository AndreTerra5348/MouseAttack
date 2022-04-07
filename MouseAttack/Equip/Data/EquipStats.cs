using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity.Player;
using MouseAttack.Misc;
using System;

namespace MouseAttack.Equip.Data
{
    public class EquipStats
    {
        public readonly static Color NormalColor = Colors.White;
        public readonly static Color GreaterColor = Colors.Green;
        public readonly static Color LesserColor = Colors.Red;
        public StatsType Type { get; private set; }
        public float Percentage { get; private set; } = 0.0f;

        PlayerEntity PlayerEntity => TreeSharer.GetNode<PlayerEntity>();
        PlayerCharacter PlayerCharacter => PlayerEntity.Character;

        public EquipStats(StatsType type, float percentage)
        {
            Type = type;
            Percentage = percentage;
        }

        public override string ToString()
        {
            return $"{StatsConstants.FullNameMap[Type]} +{Percentage}%";
        }
    }
}
