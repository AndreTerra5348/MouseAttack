using Godot;
using MouseAttack.Constants;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.World.UI
{
    public class SkipTurnButton : Button
    {
        GridController GridController => TreeSharer.GetNode<GridController>();

        public override void _Pressed()
        {
            if (GridController.IsTurnElapsing)
                return;
            GridController.ElapseTurn();
        }
            

        public override void _Input(InputEvent @event)
        {            
            if (@event.IsActionPressed(InputAction.ElapseTurn))
                _Pressed();
        }
    }
}
