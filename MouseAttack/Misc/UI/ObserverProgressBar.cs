using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc.UI
{
    public abstract class ObserverProgressBar<T> : Control where T : ObservableNode
    {
        [Export]
        NodePath _sourcePath = "";
        protected T Source { get; private set; }
        protected ProgressBar ProgressBar;
        protected abstract List<PropertyBinding> Bindings { get; }

        public override void _Ready()
        {
            Source = GetNode<T>(_sourcePath);
            Source.PropertyChanged += OnObservableNodePropertyChanged;

            ProgressBar = GetNode<ProgressBar>(nameof(ProgressBar));
            foreach (PropertyBinding binding in Bindings)
            {
                SetProperty(binding);
            }
        }

        private void OnObservableNodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            foreach(PropertyBinding binding in Bindings)
            {
                if (e.PropertyName != binding.SourcePropertyName)
                    continue;
                SetProperty(binding);
            }
        }

        private void SetProperty(PropertyBinding binding)
        {
            // Get Source's Property value
            object propertyValue = Source
                .GetType()
                .GetProperty(binding.SourcePropertyName)
                .GetValue(Source);

            // Set Progress Bar's Property value
            ProgressBar
                .GetType()
                .GetProperty(binding.TargetPropertyName)
                .SetValue(ProgressBar, propertyValue);
        }
    }
}
