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
using MouseAttack.Constants;

namespace MouseAttack.World.Monster
{
    public class MonsterSpawnedEventArgs : EventArgs
    {
        public readonly MonsterEntity Entity;
        public MonsterSpawnedEventArgs(MonsterEntity entity) => Entity = entity;
    }

    public class MonsterGenerator : Node
    {
        public event EventHandler<MonsterSpawnedEventArgs> MonsterSpawned;

        [Export]
        List<MonsterPool> _pool = new List<MonsterPool>();

        Stage _stage;
        SpawnPoint _spawnPoint;
        int _dbIndex = 0;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
            _spawnPoint = GetNode<SpawnPoint>(nameof(SpawnPoint));
            Timer timer = GetNode<Timer>(nameof(Timer));

            timer.Connect(Signals.Timeout, this, nameof(Spawn));
            timer.Start();
            _stage.Initialized += (object sender, EventArgs e) => Spawn();
        }

        void Spawn()
        {
            MonsterBody monasterBody = _pool[_dbIndex].GetRandomMonster();
            _stage.AddChild(monasterBody);
            MonsterCharacter monsterCharacter = monasterBody.Entity.Character;
            monasterBody.Position = _spawnPoint.GetRandomPosition();
            MonsterSpawned?.Invoke(this, new MonsterSpawnedEventArgs(monasterBody.Entity));
                     
        }
    }
}
