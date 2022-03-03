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
        T _character;
        public new T Character => _character ?? (_character = base.Character as T);
    }
}
