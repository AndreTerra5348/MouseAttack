using Godot;
using MouseAttack.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public class CommonAliveEntity : CommonEntity
    {
        [Export]
        ResourceData _health;

        public bool IsDead { get => _health.IsDepleted; }
        public float Health { get => _health.CurrentValue; }

        public override void _EnterTree()
        {
            // Make resource unique
            _health = new ResourceData(_health);
            _health.Depleted += OnHealthDepleted;
            base._EnterTree();
        }

        public override void _Ready()
        {
            base._Ready();
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

        protected virtual void OnDeath() { }
        protected virtual void OnHit() { }
    }
}
