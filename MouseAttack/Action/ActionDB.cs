using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action
{
    public class ActionDB : Resource
    {
        [Export]
        public List<CommonAction> Actions { get; private set; }

        public List<CommonAction> GetUnlockedActions() => Actions.Where(a => a.IsUnlocked).ToList();

        public CommonAction GetMainAttack() => Actions.First();
    }
}
