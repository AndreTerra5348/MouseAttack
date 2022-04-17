using Godot;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using MouseAttack.World.Monster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseAttack.World
{
    public class Stage : Node2D, IInitializable, ISharable, INotifyPropertyChanged
    {
        public event EventHandler Initialized;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        int _level = 1;
        public int Level
        {
            get => _level;
            set
            {
                if (_level == value)
                    return;
                _level = value;
                OnPropertyChanged();
            }
        }
        int Seed { get; set; } = 1;
        public Random Random { get; private set; } 

        public Stage()
        {
            TreeSharer.RegistryNode(this);
            Random = new Random(Seed);
        }

        public override void _Ready()
        {
            base._Ready();
            Initialized?.Invoke(this, EventArgs.Empty);
        }

        public void LevelUp() =>
            Level++;
    }
}