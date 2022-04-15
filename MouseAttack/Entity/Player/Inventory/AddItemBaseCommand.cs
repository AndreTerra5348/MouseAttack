using MouseAttack.Item.Data;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player.Inventory
{
    public abstract class AddItemBaseCommand<T> : BaseCommand where T : CommonItem
    {
        protected PlayerInventory Inventory { get; private set; }
        public AddItemBaseCommand(PlayerInventory inventory) =>
            Inventory = inventory;

        public override void Execute(object parameter) =>
            Execure(parameter as T);

        protected abstract void Execure(T item);
    }
}
