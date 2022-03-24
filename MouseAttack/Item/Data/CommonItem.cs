using Godot;
using MouseAttack.Item.Drop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Data
{
    public interface ISellable
    {
        int Price { get; }
    }

    public class CommonItem : Resource
    {
        [Export]
        PackedScene DropScene { get; set; }
        public Node2D NewDrop => DropScene.Instance<Node2D>();
        [Export]
        PackedScene IconScene { get; set; }
        public Control NewIcon => IconScene.Instance<Control>();
        [Export]
        public int Count { get; set; } = 0;
        public virtual string Tooltip { get; }
        public virtual int DropRate { get; }

        public void Add(int count = 1) => Count += count;
        public void Remove(int count = 1) => Count -= count;

        public T GetDropInstance<T>() where T : CommonDrop => 
            DropScene.Instance<T>();
    }
}