using System;
using System.Collections.Generic;

namespace MouseAttack.Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this IList<T> list, Random random)
        {
            return list[random.Next(list.Count)];
        }
    }
}
