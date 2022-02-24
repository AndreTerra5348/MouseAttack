using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseAttack.World;

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
    }
}
