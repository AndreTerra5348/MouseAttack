using Godot;
using MouseAttack.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.Castle
{
    public class CastleData : Resource
    {
        [Export]
        public ResourceData Health;

        public void ResetResources() => Health.Reset();
    }
}
