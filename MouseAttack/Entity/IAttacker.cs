using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public interface IAttacker
    {
        Stats Damage { get; }
        Stats CriticalRate { get; }
        Stats CriticalDamage { get; }
        bool IsCritical { get; }
    }
}
