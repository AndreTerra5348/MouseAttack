using Godot;
using MouseAttack.Character;
using MouseAttack.Misc;
using MouseAttack.Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public abstract class CommonAliveEntity : CommonEntity, IResourceRegenerable
    {
        [Export]
        [MakeUnique]
        ResourceData _health = null;

        public event EventHandler ResourceUsed;

        public bool IsDead { get => _health.IsDepleted; }
        public float Health { get => _health.CurrentValue; }

        public bool IsResourceFull => _health.IsFull;

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
            _health.Use(damage);
            ResourceUsed?.Invoke(this, EventArgs.Empty);
            OnHit();
        }

        protected abstract void OnDeath();
        protected abstract void OnHit();
        public virtual void Regenerate() {}
        protected void Regenerate(float value) => _health.Regenerate(value);

    }
}
