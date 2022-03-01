using Godot;
using MouseAttack.Entity.Castle;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.MonsterSystem
{
    public class MonsterSpawnedEventArgs : EventArgs
    {
        public readonly CommonMonster Monster;
        public MonsterSpawnedEventArgs(CommonMonster monster) => Monster = monster;
    }

    public class MonsterGenerator : Node
    {
        public event EventHandler<MonsterSpawnedEventArgs> MonsterSpawned;

        [Export]
        List<PackedScene> _monsters = null;
        [Export]
        NodePath _spawnPathFollowPath = null;

        int _monsterCount = 0;
        Stage _stage = null;      
        SpawnPathFollow2D _spawnPathFollow2d = null;
        public override void _Ready()
        {
            _spawnPathFollow2d = GetNode<SpawnPathFollow2D>(_spawnPathFollowPath);
            _stage = this.GetStage();
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            if (_monsterCount > 0)
                return;
            for (int i  = 0; i < _stage.Wave; i++)
            {
                Spawn();
            }
            _stage.NextWave();
        }

        void Spawn()
        {
            for(int i = 0; i < _stage.Level; i++)
            {
                var instance = _monsters[i].Instance<CommonMonster>();
                _stage.AddChild(instance);
                _monsterCount++;
                instance.Health.Depleted += (object sender, EventArgs e) => _monsterCount--;
                instance.Position = _spawnPathFollow2d.RandomPosition;
                MonsterSpawned?.Invoke(this, new MonsterSpawnedEventArgs(instance));
            }            
        }
    }
}
