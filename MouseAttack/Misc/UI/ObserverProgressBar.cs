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
        NodePath _propertyOwnerPath = "";
        protected T _propertyOwner;
        ProgressBar _bar;
        protected abstract string PropertyName { get; }
        protected abstract float MaxValue { get; }
        protected abstract float StartingValue { get; }
        public override void _Ready()
        {
            _propertyOwner = GetNode<T>(_propertyOwnerPath);
            _propertyOwner.PropertyChanged += OnObservableNodePropertyChanged;

            _bar = GetNode<ProgressBar>(nameof(ProgressBar));
            _bar.MaxValue = MaxValue;
            _bar.Value = StartingValue;
        }

        private void OnObservableNodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != PropertyName)
                return;

            PropertyInfo propertyInfo = _propertyOwner.GetType().GetProperty(PropertyName);
            object propertyValue = propertyInfo.GetValue(_propertyOwner);
            if (propertyValue.GetType() != typeof(float))
                return;

            float value = (float)propertyValue;
            _bar.Value = value;
            OnValueUpdated(value);
        }

        protected virtual void OnValueUpdated(float value) { }
    }
}
