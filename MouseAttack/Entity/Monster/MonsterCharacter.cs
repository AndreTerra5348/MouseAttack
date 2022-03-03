using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Monster
{
    public class MonsterCharacter : MortalCharacter, IDefender, IAttacker
    {
        public Stats MovementSpeed => StatsMap[StatsType.MovementSpeed];
        public Stats Damage => StatsMap[StatsType.Damage];
        public Stats Defense => StatsMap[StatsType.Defense];
        public Stats CriticalRate => StatsMap[StatsType.CriticalRate];
        public Stats CriticalDamage => StatsMap[StatsType.CriticalDamage];
        public bool IsCritical => CriticalRate.Value <= _random.Next(100);
        Random _random = new Random();
    }
}
