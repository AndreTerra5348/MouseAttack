using Godot;
using MouseAttack.Item.Data;
using MouseAttack.Misc;
using MouseAttack.Skill.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Player
{
    public class PlayerInventory : Node, ISharable
    {
        [Export]
        public List<CommonItem> Items { get; private set; } = new List<CommonItem>();
        [Export]
        public List<CommonSkill> Skills { get; private set; } = new List<CommonSkill>();
        [Export]
        public CommonItem Gold { get; private set; }

        public PlayerInventory() =>
            TreeSharer.RegistryNode(this);
    }
}
