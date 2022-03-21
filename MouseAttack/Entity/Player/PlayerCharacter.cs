using MouseAttack.Characteristic;
using System;

namespace MouseAttack.Entity.Player
{
    public class PlayerCharacter : Character
    {
        const int AttributesPerLevel = 5;

        int _attributePoints = 0;
        public int AttributePoints 
        { 
            get => _attributePoints; 
            private set
            {
                if (value == _attributePoints)
                    return;
                _attributePoints = value;
                OnPropertyChanged();
            }
        }

        public ResourcePool Mana => StatsMap[StatsType.Mana] as ResourcePool;
        public Stats ManaRegen => StatsMap[StatsType.ManaRegen];
        public Stats HealthRegen => StatsMap[StatsType.HealthRegen];

        public override void _Ready()
        {
            base._Ready();
        }

        protected override void OnLevelRaised()
        {
            AttributePoints += AttributesPerLevel;
        }

        public bool HasEnoughMana(float value) => Mana.CurrentValue >= value;
        public void UseMana(float value) => Mana.Use(value);

        public void AddAttributePoint(Stats stats)
        {
            if (AttributePoints == 0)
                return;
            stats.Points++;
            AttributePoints--;
        }

        public void RemoveAttributePoint(Stats stats)
        {
            if (stats.Points == 0)
                return;
            stats.Points--;
            AttributePoints++;
        }
    }
}
