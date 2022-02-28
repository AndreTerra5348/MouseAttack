using Godot;
using System;
using System.Linq;
using System.Reflection;
using MouseAttack.Misc;

namespace MouseAttack.Extensions
{
    public static class NodeExtensions
    {
        const string RootPathFormat = "/root/{0}";

        public static T GetAutoload<T>(this Node node) where T : Node
        {
            string path = String.Format(RootPathFormat, typeof(T).Name);
            return node.GetNode<T>(path);
        }

        public static void MakeResourcesUnique(this Node node, Type baseType)
        {
            var type = node.GetType();

            while (type != baseType)
            {
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(f => f.IsDefined(typeof(MakeUniqueAttribute), false));
                
                // Find fields that have a copy-constructor
                // instantiate the copy
                // set the copy as the new value
                foreach (var field in fields)
                {
                    var fieldType = field.FieldType;
                    var fieldValue = field.GetValue(node);
                    if (!(fieldValue is Resource))
                        continue;
                    var fieldConstructor = fieldType.GetConstructor(new[] { fieldType });
                    if (fieldConstructor == null)
                        continue;
                    var newField = fieldConstructor.Invoke(new[] { fieldValue });
                    field.SetValue(node, newField);
                }

                type = type.BaseType;
            }
        }
    }
}
