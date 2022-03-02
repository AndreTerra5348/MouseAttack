using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public class MortalCharacter : ResourcefulCharacter
    {
        public event EventHandler Dead;
        protected override StatsType ResourceType => StatsType.Health;

        public override void _Ready()
        {
            base._Ready();
            ResourcePool.Depleted += OnResourceDepleted;
        }

        private void OnResourceDepleted(object sender, EventArgs e)
        {
            ResourcePool.Depleted -= OnResourceDepleted;
            Dead?.Invoke(this, EventArgs.Empty);
        }

        public void Hit(float damage) => Use(damage);
    }
}
