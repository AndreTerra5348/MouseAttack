using System;

namespace MouseAttack.Misc
{
    public class IntegerRange
    {
        public readonly int Minimum;
        public readonly int Maximum;

        public IntegerRange(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public int GetRandom(Random random) =>
            random.Next(Minimum, Maximum);
    }
}
