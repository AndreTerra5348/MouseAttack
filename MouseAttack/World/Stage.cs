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

        public MonsterGenerator MonsterGenerator { get;  private set; }
        public PlayerCharacter Player { get; private set; }
        public CastleEntity Castle { get; private set; }

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        public override void _Ready()
        {
            base._Ready();
            MonsterGenerator = GetNode<MonsterGenerator>(nameof(MonsterGenerator));
            Player = GetNode<PlayerCharacter>(nameof(PlayerCharacter));
            Castle = GetNode<CastleEntity>(nameof(CastleEntity));
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