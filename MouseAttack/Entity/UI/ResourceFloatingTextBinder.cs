using Godot;
using MouseAttack.Characteristic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class ResourceFloatingTextBinder : Node
    {
        [Export]
        Color _increaseColor;
        [Export]
        Color _decreaseColor;
        FloatingTextSpawner _textSpawner;

        public override void _Ready()
        {
            ResourcePool resource = GetParent<ResourcePool>();
            resource.Changed += OnResourceChanged;
            _textSpawner = GetNode<FloatingTextSpawner>(nameof(FloatingTextSpawner));

        }

        private void OnResourceChanged(object sender, ResourceChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
