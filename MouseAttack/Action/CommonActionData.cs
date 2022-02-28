using Godot;
using MouseAttack.World;

namespace MouseAttack.Action
{
    public class CommonActionData : Resource
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

        public virtual void Use(Stage stage)
        {
            var instance = EffectScene.Instance<CommonAction>();
            instance.ActionData = this;
            stage.AddChild(instance);
            StartCooldown();
        }
        protected void StartCooldown() => _cooldown = true;
        public void StopCooldown() => _cooldown = false;
    }
}

