using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Subsystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity
{
    public abstract class ResourcefulCharacter : Character, IResourceRegenerable
    {
        public event EventHandler ResourceUsed;

        public ResourcePool ResourcePool { get; private set; }
        public bool IsResourceEmpty { get => ResourcePool.IsDepleted; }
        public bool IsResourceFull => ResourcePool.IsFull;

        protected abstract StatsType ResourceType { get; }

        public override void _Ready()
        {
            base._Ready();
            ResourcePool = StatsMap[ResourceType] as ResourcePool;
        }

        public void Use(float value)
        {
            if (IsResourceEmpty)
                return;
            ResourcePool.Use(value);
            ResourceUsed?.Invoke(this, EventArgs.Empty);
        }
        public virtual void Regenerate() { }
    }
}
