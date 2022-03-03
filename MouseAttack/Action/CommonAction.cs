using Godot;
using MouseAttack.Entity;
using MouseAttack.World;

namespace MouseAttack.Action
{
    /// <summary>
    /// Base class for all actions
    /// Examples of instances: Heal, Any buff
    /// </summary>
    public class CommonAction : Resource
    {
        [Export]
        public PackedScene EffectScene { get; private set; }
        [Export]
        public PackedScene ItemScene { get; private set; }
        [Export]
        public Texture Icon { get; private set; }
        [Export]
        public float Cost { get; private set; }
        [Export]
        public float CooldownTimeout { get; private set; }

        bool _cooldown = false;
        public bool OnCooldown => _cooldown;

        public T GetEffectInstance<T>() where T : CommonEffect => EffectScene.Instance<T>();
        public virtual void Use() => StartCooldown();
        protected void StartCooldown() => _cooldown = true;
        public void StopCooldown() => _cooldown = false;
    }
}

