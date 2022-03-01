using Godot;
using System;
using System.Linq;
using System.Reflection;
using MouseAttack.Misc;
using MouseAttack.World;
using MouseAttack.Entity.Castle;
using MouseAttack.Player;

namespace MouseAttack.Extensions
{
    public static class NodeExtensions
    {
        const string RootPathFormat = "/root/{0}";
        const string StagePath = "/root/Game/Stage";

        public static T GetAutoload<T>(this Node node) where T : Node
        {
            string path = String.Format(RootPathFormat, typeof(T).Name);
            return node.GetNode<T>(path);
        }

        public static Stage GetStage(this Node node)
        {
            return node.GetNode<Stage>(StagePath);
        }

        /// <summary>
        /// Uses reflection to find properties of type Resource with [MakeCopy] attribute amd Copy-Constructor
        /// with public or private set
        /// This function can be useful to make resource "unique" via code
        /// </summary>
        /// <param name="node"></param>
        /// <param name="baseType"></param>
        public static void MakeResourcesCopy(this Node node, Type baseType)
        {
            var type = node.GetType();

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
                    var propertyInstance = property.GetValue(node);
                    if (!(propertyInstance is Resource))
                        continue;
                    var propertyCopyConstructor = propertyType.GetConstructor(new[] { propertyType });
                    if (propertyCopyConstructor == null)
                        continue;
                    var newPropertyInstance = propertyCopyConstructor.Invoke(new[] { propertyInstance });
                    property.DeclaringType
                        .GetProperty(property.Name)
                        .SetValue(node, newPropertyInstance, bindingFlags, null, null, null);
                }

                type = type.BaseType;
            }
        }
    }
}
