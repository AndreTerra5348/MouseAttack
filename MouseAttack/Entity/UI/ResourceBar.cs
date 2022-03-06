using Godot;
using MouseAttack.Characteristic;
using MouseAttack.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Entity.UI
{
    public class ResourceBar : Control
    {
        [Export]
        NodePath _resourcePath = null;

        ResourcePool _resource;
        ProgressBar _bar;
        public override void _Ready()
        {
            _bar = GetNode<ProgressBar>(nameof(ProgressBar));
            _resource = GetNode<ResourcePool>(_resourcePath);
            _resource.PropertyChanged += OnResourcePropertyChanged;
            _bar.MaxValue = _resource.Value;
        }

        private void OnResourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(_resource.CurrentValue))
                return;

            _bar.Value = _resource.CurrentValue;
        }
    }
}
