using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.UI
{
    public abstract class PanelBase : PanelContainer
    {
        protected abstract string OpenInputAction { get; }

        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.IsActionPressed(OpenInputAction))
            {
                Visible = !Visible;
                if(Visible)
                    Update();
            }
        }
    }
}
