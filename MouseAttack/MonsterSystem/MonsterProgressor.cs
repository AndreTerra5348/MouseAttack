using Godot;
using MouseAttack.Characteristic;
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
        Dictionary<StatsType, float> _baseBonus = new Dictionary<StatsType, float>()
        {
            // Percentage
            { StatsType.Health, 0.01f },
            { StatsType.Damage, 0.01f },
            { StatsType.Defense, 0.01f },
            { StatsType.CriticalRate, 0.01f },
            { StatsType.CriticalDamage, 0.01f },
        };
        Dictionary<StatsType, float> _applicableBonuses = new Dictionary<StatsType, float>();
        Random _random = new Random();
        Stage _stage;

        public override void _Ready()
        {
            _stage = this.GetStage();
            _stage.Initialized += OnStageInitialized;            
        }

        private void OnStageInitialized(object sender, EventArgs e)
        {
            MonsterGenerator monsterGenerator = _stage.MonsterGenerator;
            monsterGenerator.MonsterSpawned += OnMonsterSpawned;
            _stage.LevelFinished += OnLevelFinished;
        }

        private void OnMonsterSpawned(object sender, MonsterSpawnedEventArgs e)
        {
            MonsterEntity monsterEntity = e.Entity;
            MonsterCharacter monsterCharacter = monsterEntity.Character;
            foreach (var item in _applicableBonuses)
            {
                Stats stats = monsterCharacter.StatsMap[item.Key];
                stats.SetAlteredPercentage(item.Value);
            }
        }

        private void OnLevelFinished(object sender, EventArgs e)
        {
            AddApplicableBonuses();
        }
        private void AddApplicableBonuses()
        {
            Array keys = _baseBonus.Keys.ToArray();
            var stats = (StatsType)keys.GetValue(_random.Next(keys.Length));
            var value = _baseBonus[stats] * _stage.Wave;
            _applicableBonuses[stats] = value;
        }

    }
}
