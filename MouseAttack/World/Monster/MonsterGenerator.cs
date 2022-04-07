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
using MouseAttack.Misc;

namespace MouseAttack.World.Monster
{
    public class MonsterSpawnedEventArgs : EventArgs
    {
        public readonly MonsterEntity Entity;
        public MonsterSpawnedEventArgs(MonsterEntity entity) => Entity = entity;
    }

    public class MonsterGenerator : Node, ISharable
    {
        public event EventHandler<MonsterSpawnedEventArgs> MonsterSpawned;

        public static readonly int MonsterSpawnTile = 1;

        [Export]
        List<MonsterPool> _pool = new List<MonsterPool>();

        int _dbIndex = 0;
        int _divisor = 5;
        List<Vector2> _monsterSpawnPoints;
        Stage Stage => TreeSharer.GetNode<Stage>();
        GridController GridController => TreeSharer.GetNode<GridController>();

        public MonsterGenerator() => TreeSharer.RegistryNode(this);     
        

        public override void _Ready()
        {
            base._Ready();
            _monsterSpawnPoints = GridController
                .GetUsedCellsById(MonsterSpawnTile)
                .Cast<Vector2>()
                .Select(x => GridController.MapToWorld(x))
                .ToList();

            GridController.Initialized += (s, e) => Spawn();
            GridController.RoundFinished += (s, e) =>
            {
                if (CanSpawn())
                    Spawn();
            };
        }

        bool CanSpawn() =>
            IsSpawnableRound() || 
            HasZeroMonsters();

        bool IsSpawnableRound() =>
            GridController.RoundCount % _divisor == 0;

        bool HasZeroMonsters() =>
            GridController.MonsterCount <= 1;

        Vector2 GetRandomPosition() =>
            _monsterSpawnPoints[Stage.Random.Next(_monsterSpawnPoints.Count)];

        void Spawn()
        {
            Vector2 position = GetRandomPosition();
            if (!GridController.IsCellAvailable(position))
                return;

            MonsterEntity monsterEntity = _pool[_dbIndex].GetRandomMonster();
            GridController.AddChild(monsterEntity);
            monsterEntity.Position = position;
            MonsterSpawned?.Invoke(this, new MonsterSpawnedEventArgs(monsterEntity));                     
        }
    }
}
