using Godot;
using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class ResourceFloatingTextBinder : Control
    {
        enum Options { IncreaseOnly, DecreaseOnly, Both }
        [Export]
        Options _option = Options.Both;
        [Export]
        Color _increaseColor = Colors.Green;
        [Export]
        Color _decreaseColor = Colors.Red;
        [Export]
        NodePath _resourcePath = null;
        FloatingTextSpawner _textSpawner;

        public override void _Ready()
        {
            _textSpawner = GetNode<FloatingTextSpawner>(nameof(FloatingTextSpawner));
            ResourcePool resource = GetNode<ResourcePool>(_resourcePath);
            resource.Changed += OnResourceChanged;
        }

        private void OnResourceChanged(object sender, ResourceChangedEventArgs e)
        {
            if (_option == Options.IncreaseOnly && !e.Increased)
                return;
            if (_option == Options.DecreaseOnly && e.Increased)
                return;

            Color color = e.Increased ? _increaseColor : _decreaseColor;
            string valueText = e.Value.ToString();
            _textSpawner.Spawn(valueText, color);
        }
    }
}
