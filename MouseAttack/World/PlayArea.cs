using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World
{
    public class PlayArea : Control, ISharable
    {

        public event EventHandler PlayAreaEntered;
        public event EventHandler PlayAreaExited;
        public bool OnPlayArea { get; private set; } = false;
        public PlayArea() => TreeSharer.RegistryNode(this);

        public override void _EnterTree()
        {
            Connect(Signals.MouseEntered, this, nameof(OnMouseEntered));
            Connect(Signals.MouseExited, this, nameof(OnMouseExited));
        }
        public override void _Ready()
        {
            this.SkipNextFrame();
        }
        private void OnMouseEntered()
        {
            OnPlayArea = true;
            PlayAreaEntered?.Invoke(this, EventArgs.Empty);
        }

        private void OnMouseExited()
        {
            OnPlayArea = false;
            PlayAreaExited?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 ClampPosition(Vector2 position, Vector2 size)
        {
            if (position.x < RectGlobalPosition.x)
                position.x = RectGlobalPosition.x;
            else if (position.x + size.x > RectGlobalPosition.x + RectSize.x)
                position.x = RectGlobalPosition.x + RectSize.x - size.x;

            if (position.y < RectGlobalPosition.y)
                position.y = RectGlobalPosition.y;
            else if (position.y + size.y > RectGlobalPosition.y + RectSize.y)
                position.y = RectGlobalPosition.y + RectSize.y - size.y;

            return position;
        }
    }
}
