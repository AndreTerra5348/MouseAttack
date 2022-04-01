using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World.Monster;
using System;
using System.Collections.Generic;

namespace MouseAttack.World
{
    public class Stage : Node2D, IInitializable, ISharable
    {
        public event EventHandler LevelFinished;
        public event EventHandler Initialized;

        public int Wave { get; private set; } = 1;
        public int Level { get; private set; } = 1;

        int _wavesPerLevel = 10;
        const int SEED = 1;
        public Random Random { get; private set; } = new Random(SEED);
        public Stage() => TreeSharer.RegistryNode(this);

        public override void _Ready()
        {
            base._Ready();
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