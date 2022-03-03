using Godot;
using MouseAttack.Entity.Castle;
using MouseAttack.Entity.Player;
using MouseAttack.MonsterSystem;
using System;

namespace MouseAttack.World
{
    public class Stage : Node2D
    {
        public event EventHandler LevelFinished;
        public event EventHandler StageInitialized;

        public MonsterGenerator MonsterGenerator { get;  private set; }
        public MonsterProgressor MonsterProgressor { get; private set; }
        public PlayerCharacter PlayerCharacter { get; private set; }
        public CastleEntity CastleEntity { get; private set; }

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        public override void _Ready()
        {
            base._Ready();
            MonsterGenerator = GetNode<MonsterGenerator>(nameof(MonsterGenerator));
            PlayerCharacter = GetNode<PlayerCharacter>(nameof(PlayerCharacter));
            CastleEntity = GetNode<CastleEntity>(nameof(CastleEntity));
            MonsterProgressor = GetNode<MonsterProgressor>(nameof(MonsterProgressor));
            StageInitialized?.Invoke(this, EventArgs.Empty);
        }

        public void NextWave()
        {
            Wave++;
            if (Wave == _wavesPerLevel)
            {
                LevelFinished?.Invoke(this, EventArgs.Empty);
                NextLevel();
            }
                
        }

        public void NextLevel()
        {
            Level++;
            Wave = 0;
        }
    }
}