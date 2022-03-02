using Godot;
using System;
using MouseAttack.Misc;
using MouseAttack.World;

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
    }
}
