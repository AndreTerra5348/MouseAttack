using Godot;
using MouseAttack.Extensions;
using MouseAttack.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class FloatingTextSpawner : Control
    {
        [Export]
        PackedScene _floatingText = null;
        Stage _stage;

        public override void _Ready()
        {
            base._Ready();
            _stage = this.GetStage();
        }

        public void Spawn(string text, Color color)
        {
            var instance = _floatingText.Instance<FloatingText>();
            instance.Text = text;
            instance.Color = color;
            instance.RectGlobalPosition = RectGlobalPosition;
            _stage.AddChild(instance);
        }

    }
}
