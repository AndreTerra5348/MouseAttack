using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.World;
using MouseAttack.World.Monster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public interface ISharable
    {        
    }
    public class TreeSharer
    {
        static TreeSharer _instance;
        public static TreeSharer Instance => _instance ?? (_instance = new TreeSharer());

        Dictionary<Type, ISharable> _nodeMap = new Dictionary<Type, ISharable>();

        public static T GetNode<T>() where T : Node, ISharable =>
            Instance._nodeMap[typeof(T)] as T;
        public static void RegistryNode(ISharable sharable) =>
            Instance._nodeMap.Add(sharable.GetType(), sharable);
    }
}
