using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    /// <summary>
    /// Facilitate Character usage without the need for constantly casting
    /// </summary>
    /// <typeparam name="T">Character type</typeparam>
    public abstract class SpecializedEntity<T> : CommonEntity where T : Character
    {
        public new T Character => base.Character as T;
    }
}
