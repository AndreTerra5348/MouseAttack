using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Extensions
{
    public static class GodotArrayExtensions
    {
        public static T First<T>(this Godot.Collections.Array array)
        {
            if (array.Count == 0)
                throw new ArgumentOutOfRangeException();
            return (T)array[0];
        }
    }
}
