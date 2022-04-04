using Godot;

namespace MouseAttack.Item.Icon
{
    public struct IconColorInfo
    {
        public Color Border { get; private set; }
        public Color Background { get; private set; }
        public IconColorInfo(Color border, Color background)
        {
            Border = border;
            Background = background;
        }

    }
}