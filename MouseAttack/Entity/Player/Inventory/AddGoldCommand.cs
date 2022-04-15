using Godot;
using MouseAttack.Item.Currency;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.Inventory
{
    public class AddGoldCommand : AddItemBaseCommand<Gold>
    {
        public AddGoldCommand(PlayerInventory inventory) : base(inventory)
        {
        }

        protected override void Execure(Gold item) =>
            Inventory.Gold.Count += item.Count;
    }
}
