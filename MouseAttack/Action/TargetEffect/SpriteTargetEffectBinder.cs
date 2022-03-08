using Godot;
using MouseAttack.Action.WorldEffect;

namespace MouseAttack.Action.TargetEffect
{
    public enum ZIndex { Behind, InFront }
    public class SpriteTargetEffectBinder : TargetEffectBinder<Sprite>
    {
        [Export]
        ZIndex _zIndex = ZIndex.Behind;
        protected override void OnBeforeAddChild(Sprite instance, ActionEventArgs e)
        {
            instance.GlobalPosition = e.Target.GlobalPosition;
            instance.ZIndex = _zIndex == ZIndex.Behind ? e.Target.ZIndex - 1 : e.Target.ZIndex + 1;
        }
    }
}
