using Godot;
using MouseAttack.World.Autoload;

namespace MouseAttack.Action
{
    public class CommonActionData : Resource
    {
        [Export]
        public PackedScene EffectScene;
        [Export]
        public PackedScene ItemScene;
        [Export]
        public Texture Icon;
        [Export]
        public float Cost;
        [Export]
        public float CooldownTimeout;

        bool cooldown = false;

        public virtual void Use() => StartCooldown();
        protected void StartCooldown() => cooldown = true;
        public void StopCooldown() => cooldown = false;
        public bool OnCooldown() => cooldown;

        public CommonAction GetEffectInstance() => EffectScene.Instance<CommonAction>();

    }
}

