using Godot;
using System;

namespace MouseAttack.Misc
{
    public class IntegerRange : Resource
    {
        [Export]
        public int Minimum { get; private set; }
        [Export]
        public int Maximum { get; private set; }

        public IntegerRange()
        {

        }
        public IntegerRange(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public int GetRandom(Random random) =>
            random.Next(Minimum, Maximum);
    }
}
