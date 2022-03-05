using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Misc;
using MouseAttack.MonsterSystem;
using System;

namespace MouseAttack.World
{
    public class Stage : Node2D, IInitializable
    {
        public event EventHandler LevelFinished;
        public event EventHandler Initialized;

        public MonsterGenerator MonsterGenerator { get;  private set; }
        public MonsterProgressor MonsterProgressor { get; private set; }
        public PlayerEntity PlayerEntity { get; private set; }

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        public override void _Ready()
        {
            base._Ready();
            PlayerEntity = GetNode<PlayerEntity>(nameof(PlayerEntity));
            MonsterGenerator = GetNode<MonsterGenerator>(nameof(MonsterGenerator));
            MonsterProgressor = GetNode<MonsterProgressor>(nameof(MonsterProgressor));
            Initialized?.Invoke(this, EventArgs.Empty);
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