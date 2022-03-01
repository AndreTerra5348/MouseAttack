using Godot;
using MouseAttack.Character;
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
    public class MonsterProgressor : Node
    {
        [Export]
        NodePath _monsterGeneratorPath = null;
        
        Dictionary<StatsType, float> _baseBonus = new Dictionary<StatsType, float>()
        {
            // Percentage
            { StatsType.Health, 0.01f },
            { StatsType.Damage, 0.01f },
            { StatsType.Defense, 0.01f },
            { StatsType.CriticalRate, 0.01f },
            { StatsType.CriticalDamage, 0.01f },
        };

        List<KeyValuePair<StatsType, float>> _applicableBonuses = new List<KeyValuePair<StatsType, float>>();

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
                Stats stats = monster.StatsMap[item.Key];
                stats.SetAlteredPercentage(item.Value);
            }
        }

        private void OnLevelFinished(object sender, EventArgs e)
        {
            var stats = GetStats();
            var value = GetPercentage(stats);
            _applicableBonuses.Add(new KeyValuePair<StatsType, float>(stats, value));
        }

        private StatsType GetStats()
        {
            Array values = _baseBonus.Keys.ToArray();
            return (StatsType)values.GetValue(_random.Next(values.Length));
        }

        private float GetPercentage(StatsType stats)
        {
            var bonusMultiplier = _stage.Wave;
            return _baseBonus[stats] * bonusMultiplier;
        }

    }
}
