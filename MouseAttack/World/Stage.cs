using Godot;
using MouseAttack.Entity.Castle;
using MouseAttack.Player;
using System;

namespace MouseAttack.World
{
    public class Stage : Node2D
    {
        public event EventHandler LevelFinished;
        [Export]
        NodePath _monsterGeneratorPath = null;
        [Export]
        NodePath _playerPath = null;
        [Export]
        NodePath _castlePath = null;

        public MonsterGenerator MonsterGenerator { get;  private set; }
        public PlayerEntity Player { get; private set; }
        public CastleEntity Castle { get; private set; }

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        public override void _Ready()
        {
            base._Ready();
            MonsterGenerator = GetNode<MonsterGenerator>(_monsterGeneratorPath);
            Player = GetNode<PlayerEntity>(_playerPath);
            Castle = GetNode<CastleEntity>(_castlePath);
        }

        public void NextWave()
        {
            Wave++;
            if (Wave == _wavesPerLevel)
                LevelFinished?.Invoke(this, EventArgs.Empty);
        }

        public void NextLevel()
        {
            Level++;
            Wave = 0;
        }
    }
}