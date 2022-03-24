using Godot;
using MouseAttack.Constants;
using MouseAttack.Entity.Player;
using MouseAttack.Extensions;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Item.Drop
{
    public abstract class CommonDrop : Node2D
    {
        public CommonItem ItemData { get; set; }
        protected PlayerInventory PlayerInventory => TreeSharer.GetNode<PlayerInventory>();
        public override void _Input(InputEvent @event)
        {
            if (!@event.IsActionPressed(InputAction.RMB))
                return;

            if (Position != GetViewport().GetSnappedMousePosition(Values.CellSize))
                return;

            ItemPickup();
            QueueFree();
        }

        protected abstract void ItemPickup();

        
    }
}
