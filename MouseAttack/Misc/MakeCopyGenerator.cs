using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public class MakeCopyGenerator
    {
        /// <summary>
        /// Uses reflection to find properties with [MakeCopy] attribute amd Copy-Constructor
        /// with public or private set.
        /// This function can be useful to make resource "unique" via code
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="baseType"></param>
        public static void Generate(object obj, Type baseType)
        {
            var type = obj.GetType();

            while (type != baseType)
            {
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                var properties = type.GetProperties(bindingFlags)
                    .Where(f => f.IsDefined(typeof(MakeCopyAttribute), false));

                // Find properties that have a copy-constructor
                // instantiate the copy
                // set the copy as the new value
                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;
                    var propertyInstance = property.GetValue(obj);
                    if (propertyInstance == null)
                        continue;                    
                    var propertyCopyConstructor = propertyType.GetConstructor(new[] { propertyType });
                    if (propertyCopyConstructor == null)
                        continue;
                    var newPropertyInstance = propertyCopyConstructor.Invoke(new[] { propertyInstance });
                    property.DeclaringType
                        .GetProperty(property.Name)
                        .SetValue(obj, newPropertyInstance, bindingFlags, null, null, null);
                }

                type = type.BaseType;
            }
        }
    }
}
