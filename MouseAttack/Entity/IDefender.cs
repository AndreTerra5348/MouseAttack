using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public interface IDefender
    {
        Stats Defense { get; }
        void Hit(float value);
    }
}
