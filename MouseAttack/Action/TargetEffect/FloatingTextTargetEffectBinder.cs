using Godot;
using MouseAttack.Action.TargetEffect.UI;
using MouseAttack.Action.WorldEffect;

namespace MouseAttack.Action.TargetEffect
{
    public class FloatingTextTargetEffectBinder : TargetEffectBinder<FloatingLabel>
    {
        [Export]
        Color _color = Colors.Green;
        protected override void OnBeforeAddChild(FloatingLabel instance, ActionEventArgs e)
        {
            instance.Text = e.Value.ToString();
            instance.Color = _color;
            instance.RectGlobalPosition = e.Target.GlobalPosition;
        }
    }
}
