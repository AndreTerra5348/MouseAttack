using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Misc;
using MouseAttack.World.Monster;
using System;

namespace MouseAttack.World
{
    public class Stage : Node2D, IInitializable
    {
        public event EventHandler LevelFinished;
        public event EventHandler Initialized;

        [Export]
        NodePath _playerEntityPath = "";
        [Export]
        NodePath _monsterGeneratorPath = "";
        [Export]
        NodePath _monsterProgressorPath = "";
        [Export]
        NodePath _ySortPath = "";
        YSort _ySort;
        public MonsterGenerator MonsterGenerator { get;  private set; }
        public MonsterProgressor MonsterProgressor { get; private set; }
        public PlayerEntity PlayerEntity { get; private set; }

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        public override void _Ready()
        {
            base._Ready();
            PlayerEntity = GetNode<PlayerEntity>(_playerEntityPath);
            MonsterGenerator = GetNode<MonsterGenerator>(_monsterGeneratorPath);
            MonsterProgressor = GetNode<MonsterProgressor>(_monsterProgressorPath);
            _ySort = GetNode<YSort>(_ySortPath);
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

        public new void AddChild(Node node, bool legibleUniqueName = false)
        {
            _ySort.AddChild(node);
        }
    }
}