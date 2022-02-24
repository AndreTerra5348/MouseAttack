using Godot;
using System;

namespace MouseAttack.Character
{
    public class StatsData : Resource
    {
        [Export]
        public int ValuePerPoint = 1;
        [Export]
        public int Points = 0;

        public int Value { get => Points * ValuePerPoint - AbnormalValue; }
        public int AbnormalValue = 0;

    }
}
