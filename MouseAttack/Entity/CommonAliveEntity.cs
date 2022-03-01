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
        public event EventHandler ResourceUsed;

        [Export]
        [MakeCopy]
        public ResourcePool Health { get; private set; }

        public Dictionary<StatsType, Stats> StatsMap { get; private set; }

        public bool IsDead { get => Health.IsDepleted; }

        public bool IsResourceFull => Health.IsFull;

        public override void _EnterTree()
        {
            base._EnterTree();
            Health.Depleted += OnHealthDepleted;
            StatsMap = StatsMapBuilder.Build(this);
        }

        private void OnHealthDepleted(object sender, EventArgs e)
        {
            Health.Depleted -= OnHealthDepleted;
            OnDeath();
        }

        public void Hit(float damage)
        {
            if (IsDead)
                return;
            Health.Use(damage);
            ResourceUsed?.Invoke(this, EventArgs.Empty);
            OnHit();
        }

        protected abstract void OnDeath();
        protected abstract void OnHit();
        public virtual void Regenerate() {}
        protected void Regenerate(float value) => Health.Regenerate(value);

    }
}
