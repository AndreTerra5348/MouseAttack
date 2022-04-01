using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Linq;
using System.Reflection;

namespace MouseAttack.Item.Data
{
    public abstract class CommonItemFactory : Node
    {
        [AssignTo(nameof(CommonItem.Name))]
        [Export]
        public string ItemName { get; private set; }
        [AssignTo(nameof(CommonItem.Value))]
        [Export]
        public int Value { get; private set; }
        [Export]
        public int DropRate { get; private set; }
        [AssignTo(nameof(CommonItem.FloatingLabelDropScene))]
        [Export]
        public PackedScene FloatingLabelDropScene { get; private set; }
        [AssignTo(nameof(CommonItem.IconScene))]
        [Export]
        public PackedScene IconScene { get; private set; }
        [AssignTo(nameof(CommonItem.IconTexture))]
        [Export]
        public Texture IconTexture { get; private set; }
        [AssignTo(nameof(CommonItem.TooltipScene))]
        [Export]
        public PackedScene TooltipScene { get; private set; }
        

        protected Random Random => TreeSharer.GetNode<Stage>().Random;
        public virtual bool Dropped => DropRate > Random.Next(100);

        protected abstract CommonItem GetNewItem();

        public T CreateItem<T>() where T : CommonItem
        {
            CommonItem instance = GetNewItem();

            var factoryProperties = GetType()
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(AssignToAttribute)));


            BindingFlags bindings = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            foreach (var factoryProperty in factoryProperties)
            {
                var factoryValue = factoryProperty.GetValue(this);
                var attribute = factoryProperty.GetCustomAttribute<AssignToAttribute>();
                var instanceProperty = instance
                    .GetType()
                    .GetProperty(attribute.PropertyName, bindings);
                if(!instanceProperty.CanWrite)
                    instanceProperty = instanceProperty
                        .DeclaringType
                        .GetProperty(attribute.PropertyName, bindings);
                instanceProperty?.SetValue(instance, factoryValue);
            }

            return instance as T;
        }
    }
}