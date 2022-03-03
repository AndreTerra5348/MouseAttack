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
        Stage Stage => _stage ?? (_stage = this.GetStage());

        public override void _Ready()
        {
            Stage.Initialized += OnStageInitialized;            
        }

        private void OnStageInitialized(object sender, EventArgs e)
        {
            MonsterGenerator monsterGenerator = Stage.MonsterGenerator;
            monsterGenerator.MonsterSpawned += OnMonsterSpawned;
            Stage.LevelFinished += OnLevelFinished;
        }

        private void OnMonsterSpawned(object sender, MonsterSpawnedEventArgs e)
        {
            MonsterEntity monster = e.Monster;
            MonsterCharacter monsterCharacter = monster.Character;
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
            var value = _baseBonus[stats] * Stage.Wave;
            _applicableBonuses[stats] = value;
        }

    }
}
