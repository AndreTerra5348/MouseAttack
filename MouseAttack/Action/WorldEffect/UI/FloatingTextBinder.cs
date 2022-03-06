using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Action.WorldEffect.UI
{
    public class FloatingTextBinder : Node
    {
        [Export]
        Color _color = Colors.Green;
        [Export]
        PackedScene _floatingText;
        Stage _stage;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
            CommonEffect effect = GetParent<CommonEffect>();
            effect.ActionApplied += OnActionApplied;
        }

        private void OnActionApplied(object sender, ActionEventArgs e)
        {
            var instance = _floatingText.Instance<FloatingText>();
            instance.Text = e.Value.ToString();
            instance.Color = _color;
            instance.RectGlobalPosition = e.Target.GlobalPosition;
            _stage.AddChild(instance);
        }
    }
}
