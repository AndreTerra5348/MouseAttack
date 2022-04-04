using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;

namespace MouseAttack.Item.Provider
{
    public abstract class ItemResourceProvider : Node, ISharable
    {
        protected abstract Dictionary<Type, PackedScene> SceneMap { get; set; }
        protected abstract PackedScene DefaultScene { get; }
        protected PackedScene GetScene(CommonItem item) =>
            SceneMap.ContainsKey(item.GetType()) ? SceneMap[item.GetType()] : DefaultScene;
        public ItemResourceProvider() =>
            TreeSharer.RegistryNode(this);
       
    }
}
