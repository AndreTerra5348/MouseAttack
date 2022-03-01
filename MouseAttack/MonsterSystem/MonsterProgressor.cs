using Godot;
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
    public class MonsterProgressor : Node2D
    {
        [Export]
        NodePath _monsterGeneratorPath = null;
        
        Dictionary<MonsterStats, float> _baseBonus = new Dictionary<MonsterStats, float>()
        {
            // Percentage
            { MonsterStats.Health, 0.01f },
            { MonsterStats.Damage, 0.01f },
            { MonsterStats.Defense, 0.01f },
            { MonsterStats.CriticalRate, 0.01f },
            { MonsterStats.CriticalDamage, 0.01f },
        };

        List<KeyValuePair<MonsterStats, float>> _applicableBonuses = new List<KeyValuePair<MonsterStats, float>>();

        Random _random = new Random();
        Stage _stage;
        public override void _Ready()
        {
            MonsterGenerator monsterGenerator = GetNode<MonsterGenerator>(_monsterGeneratorPath);
            monsterGenerator.MonsterSpawned += OnMonsterSpawned;
            _stage = this.GetStage();
            _stage.LevelFinished += OnLevelFinished;
        }

        

        private void OnMonsterSpawned(object sender, MonsterSpawnedEventArgs e)
        {
            CommonMonster monster = e.Monster;
            foreach (var item in _applicableBonuses)
            {
                monster.ApplyBonus(item.Key, item.Value);
            }
        }

        private void OnLevelFinished(object sender, EventArgs e)
        {
            var stats = GetStats();
            var value = GetPercentage(stats);
            AddApplicableBonus(stats, value);
        }

        private MonsterStats GetStats()
        {
            Array values = Enum.GetValues(typeof(MonsterStats));
            return (MonsterStats)values.GetValue(_random.Next(values.Length));
        }

        private float GetPercentage(MonsterStats stats)
        {
            var bonusMultiplier = _stage.Wave;
            return _baseBonus[stats] * bonusMultiplier;
        }

        private void AddApplicableBonus(MonsterStats stats, float value)
        {
            _applicableBonuses.Add(new KeyValuePair<MonsterStats, float>(stats, value));
        }

    }
}
