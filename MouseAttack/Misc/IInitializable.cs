using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public interface IInitializable
    {
        event EventHandler Initialized;
    }
}
