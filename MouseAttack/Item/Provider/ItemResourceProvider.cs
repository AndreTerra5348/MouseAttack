using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;

namespace MouseAttack.Item.Provider
{
    public abstract class ItemResourceProvider<T> : SharableNode
    {        
        protected abstract Dictionary<Type, T> ResourceMap { get; set; }
        protected abstract T DefaultResource { get; }
        protected T GetResource(CommonItem item) =>
            ResourceMap.ContainsKey(item.GetType()) ? ResourceMap[item.GetType()] : DefaultResource;
    }
}
