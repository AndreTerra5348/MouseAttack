using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Entity.Monster;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.Monster
{
    public class MonsterProgressor : Node
    {
        public event EventHandler ApplicableBonuschanged;
        public Dictionary<StatsType, float> ApplicableBonuses { get; private set; } = new Dictionary<StatsType, float>();
        Dictionary<StatsType, float> _baseBonus = new Dictionary<StatsType, float>()
        {
            // Percentage
            { StatsType.Health, 1 },
            { StatsType.Damage, 1 },
            { StatsType.Defense, 1 },
            { StatsType.CriticalRate, 1 },
            { StatsType.CriticalDamage, 1 },
            { StatsType.MovementSpeed, 1 }
        };
        Stage Stage => 
            TreeSharer.GetNode<Stage>();
        MonsterGenerator MonsterGenerator => 
            TreeSharer.GetNode<MonsterGenerator>();

        int _monsterCount = 0;

        public override void _Ready() =>
            MonsterGenerator.MonsterSpawned += OnMonsterSpawned;

        private void OnMonsterSpawned(object sender, MonsterSpawnedEventArgs e)
        {
            MonsterEntity monsterEntity = e.Entity;
            MonsterCharacter monsterCharacter = monsterEntity.Character;
            foreach (var item in ApplicableBonuses)
            {
                Stats stats = monsterCharacter.GetStats(item.Key);
                stats.AlteredPercentage = item.Value;
            }
            _monsterCount++;
            if (_monsterCount % 5 == 0)
                AddApplicableBonuses();
        }

        private void AddApplicableBonuses()
        {
            Array keys = _baseBonus.Keys.ToArray();
            var stats = (StatsType)keys.GetValue(Stage.Random.Next(keys.Length));
            var value = _baseBonus[stats] * _monsterCount / 5;
            ApplicableBonuses[stats] = value;
            ApplicableBonuschanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
