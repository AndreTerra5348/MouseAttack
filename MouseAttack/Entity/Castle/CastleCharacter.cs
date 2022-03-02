using MouseAttack.Characteristic;
using MouseAttack.Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Castle
{
    public class CastleCharacter : MortalCharacter
    {
        public Stats HealthRegen => StatsMap[StatsType.HealthRegen];
        public Stats Defense => StatsMap[StatsType.Defense];

        public override void _Ready()
        {
            base._Ready();
            AddChild(new ResourceRegenerator(this));
        }

        public override void Regenerate() => ResourcePool.Regenerate(HealthRegen.Value);
    }
}
