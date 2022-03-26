using Godot;
using MouseAttack.Entity;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Skill.TargetEffect
{
    public class TargetEffectSpawner : Resource
    {
        [Export]
        PackedScene _scene = null;
        [Export(PropertyHint.Range, "0,100,")]
        int _spawnChance = 100;
        [Export]
        bool _behindeParent = true;
        Random _random = new Random();

        GridController GridController => TreeSharer.GetNode<GridController>();
        public void Spawn(CommonEntity target)
        {
            if (_spawnChance < _random.Next(100))
                return;
            var instance = _scene.Instance<Node2D>();
            instance.ZIndex = _behindeParent ? target.ZIndex - 1 : target.ZIndex + 1;
            instance.Position = target.Position;
            GridController.AddChild(instance);
        }          
    }
}
