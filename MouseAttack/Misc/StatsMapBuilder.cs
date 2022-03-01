using MouseAttack.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public static class StatsMapBuilder
    {
        public static Dictionary<StatsType, Stats> Build(object obj)
        {
            Dictionary<StatsType, Stats> map = new Dictionary<StatsType, Stats>();

            var type = obj.GetType();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var properties = type.GetProperties(bindingFlags);

            foreach (var property in properties)
            {
                var propertyInstance = property.GetValue(obj) as Stats;
                if (propertyInstance == null)
                    continue;
                map.Add(propertyInstance.Type, propertyInstance);
            }

            return map;
        }
    }
}
