using Godot;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MouseAttack.Item.Data
{
    public abstract class CommonItemFactory : Node
    {
        [AssignTo(nameof(CommonItem.Name))]
        [Export]
        public virtual string ItemName { get; protected set; }
        [AssignTo(nameof(CommonItem.Value))]
        [Export]
        public int Value { get; protected set; }        
        [AssignTo(nameof(CommonItem.IconTexture))]
        [Export]
        public List<Texture> IconTexture { get; private set; }
        [AssignTo(nameof(CommonItem.IsKnown))]
        [Export]
        public bool IsKnown { get; set; } = true;
        [AssignTo(nameof(CommonItem.Color))]
        [Export]
        public Color Color { get; set; } = new Color("a8a8a8");


        [Export]
        public int DropRate { get; private set; }
        protected Random Random => TreeSharer.GetNode<Stage>().Random;
        public virtual bool Dropped => DropRate > Random.Next(100);

        protected abstract CommonItem GetNewItem();

        public virtual T CreateItem<T>(int monsterLevel = 1) where T : CommonItem
        {
            CommonItem instance = GetNewItem();
            instance.MonsterLevel = monsterLevel;

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