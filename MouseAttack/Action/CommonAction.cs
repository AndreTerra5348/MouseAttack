using Godot;
using MouseAttack.Action.WorldEffect;
using MouseAttack.Entity;
using MouseAttack.World;

namespace MouseAttack.Action
{
    /// <summary>
    /// Base class for all actions
    /// </summary>
    public abstract class CommonAction : Resource
    {
        [Export]
        public PackedScene WorldEffectScene { get; private set; }
        [Export]
        public PackedScene ItemScene { get; private set; }
        [Export]
        public Texture Icon { get; private set; }
        [Export]
        public float Cost { get; private set; }
        [Export]
        public float CooldownTimeout { get; private set; }
        [Export]
        public bool IsUnlocked { get; private set; }

        bool _cooldown = false;
        public bool OnCooldown => _cooldown;

        public T GetWorldEffectInstance<T>() where T : CommonWorldEffect => WorldEffectScene.Instance<T>();
        public virtual void Use() => StartCooldown();
        protected void StartCooldown() => _cooldown = true;
        public void StopCooldown() => _cooldown = false;
    }
}

