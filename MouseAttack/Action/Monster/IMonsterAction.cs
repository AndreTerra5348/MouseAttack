using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action.Monster
{
    public interface IMonsterAction
    {
        float Range { get; }
        float Speed { get; }
    }
}
