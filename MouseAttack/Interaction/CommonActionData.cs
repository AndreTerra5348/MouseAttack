using Godot;
using MouseAttack.World;

namespace MouseAttack.Interaction
{
    public class CommonActionData : Resource
    {
        [Export]
        public PackedScene Scene;
        [Export]
        public float Cost;
        [Export]
        public float CooldownTimeout;

        bool cooldown = false;

        public virtual void Use() => StartCooldown();
        protected void StartCooldown() => cooldown = true;
        public void StopCooldown() => cooldown = false;
        public bool OnCooldown() => cooldown;

        public void Instantiate(WorldProxy worldProxy, Vector2 position)
        {
            var instance = Scene.Instance<CommonAction>();
            worldProxy.AddChild(instance);
            instance.Position = position;
            instance.Initialize(this);
        }
    }
}

