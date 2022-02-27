using Godot;
using MouseAttack.Character;
using MouseAttack.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public abstract class CommonAliveEntity : CommonEntity
    {
        [Export]
        [MakeUnique]
        ResourceData _health;

        public bool IsDead { get => _health.IsDepleted; }
        public float Health { get => _health.CurrentValue; }

        public override void _EnterTree()
        {
            base._EnterTree();
            _health.Depleted += OnHealthDepleted;
        }

        private void OnHealthDepleted(object sender, EventArgs e)
        {
            _health.Depleted -= OnHealthDepleted;
            OnDeath();
        }

        public void Hit(float damage)
        {
            _health.Decrease(damage);
            OnHit();
        }

        protected abstract void OnDeath();
        protected abstract void OnHit();
    }
}
