using Godot;
using MouseAttack.Constants;
using MouseAttack.Extensions;
using MouseAttack.Item.Currency;
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
    public class PlayerInventory : Node, ISharable, IInitializable
    {
        public event EventHandler Initialized;
        public ObservableCollection<CommonItem> Items { get; set; } = new ObservableCollection<CommonItem>();
        [Export]
        NodePath MainAttackFactoryPath { get; set; } = ""; 
        public DamageSkill MainAttack { get; private set; }
        public Gold Gold { get; private set; } = new Gold();

        public PlayerInventory() =>
            TreeSharer.RegistryNode(this);

        public override void _Ready()
        {
            Items.Add(MainAttack = GetNode<DamageSkillFactory>(MainAttackFactoryPath).CreateItem<DamageSkill>());
            Initialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
