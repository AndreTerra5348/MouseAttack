using Godot;
using MouseAttack.Entity.Player;
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
        public readonly MonsterEntity Monster;
        public MonsterSpawnedEventArgs(MonsterEntity monster) => Monster = monster;
    }

    public class MonsterGenerator : Node
    {
        public event EventHandler<MonsterSpawnedEventArgs> MonsterSpawned;

        [Export]
        List<PackedScene> _monsters = null;

        Stage _stage;
        Stage Stage => _stage ?? (_stage = this.GetStage());

        SpawnPoint _spawnPoint;
        SpawnPoint SpawnPoint => _spawnPoint ??
            (_spawnPoint = GetNode<SpawnPoint>(nameof(SpawnPoint)));
        
        int _monsterCount = 0;

        public override void _Process(float delta)
        {
            base._Process(delta);
            if (_monsterCount > 0)
                return;
            for (int i  = 0; i < Stage.Wave; i++)
            {
                Spawn();
            }
            Stage.NextWave();
        }

        void Spawn()
        {
            for(int i = 0; i < Stage.Level; i++)
            {
                if (i >= _monsters.Count)
                    continue;
                _monsterCount++;
                MonsterEntity monsterEntity = _monsters[i].Instance<MonsterEntity>();
                Stage.AddChild(monsterEntity);
                MonsterCharacter monsterCharacter = monsterEntity.Character;
                monsterCharacter.Dead += (object sender, EventArgs e) => _monsterCount--;
                monsterEntity.Position = SpawnPoint.RandomPosition;
                MonsterSpawned?.Invoke(this, new MonsterSpawnedEventArgs(monsterEntity));
            }            
        }
    }
}
