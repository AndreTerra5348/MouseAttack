using Godot;
using MouseAttack.Constants;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player
{
    public class PlayerInventory : Node, ISharable
    {
        public ObservableCollection<CommonItem> Items { get; set; } = new ObservableCollection<CommonItem>();
        [Export]
        public CommonSkill MainAttack { get; private set; }
        [Export]
        public CommonItem Gold { get; private set; }


        public PlayerInventory() =>
            TreeSharer.RegistryNode(this);

        public void AddMainAttack() =>
            Items.Add(MainAttack);
    }
}
