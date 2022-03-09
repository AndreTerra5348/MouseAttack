using System;
using System.ComponentModel;
using System.Reflection;

namespace MouseAttack.Misc
{
    public delegate object ConvertProperty(object value);
    public class Binding
    {
        readonly INotifyPropertyChanged _source;
        readonly string _sourcePropertyName;
        readonly object _target;
        readonly string _targetPropertyName;
        event ConvertProperty _convertProperty;

        public Binding(INotifyPropertyChanged source, 
            string sourcePropertyName, 
            object target, 
            string targetPropertyName,
            ConvertProperty convertProperty = null)
        {
            _source = source;
            _sourcePropertyName = sourcePropertyName;
            _target = target;
            _targetPropertyName = targetPropertyName;
            _convertProperty = convertProperty;

            _source.PropertyChanged += OnSourcePropertyChanged;
        }

        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != _sourcePropertyName)
                return;
            SetProperty();
        }

        public void SetProperty()
        {
            // Get Source's Property value
            object sourcePropertyValue = _source
                .GetType()
                .GetProperty(_sourcePropertyName)
                .GetValue(_source);


            // Set Target's Property value
            PropertyInfo targetPropertyInfo = _target
                .GetType()
                .GetProperty(_targetPropertyName);

            targetPropertyInfo
                .SetValue(_target, ConvertProperty(targetPropertyInfo.PropertyType, sourcePropertyValue));
        }

        private object ConvertProperty(Type targetPropertyType, object sourcePropertyValue)
        {
            return _convertProperty != null ?
                _convertProperty(sourcePropertyValue) :
                Convert.ChangeType(sourcePropertyValue, targetPropertyType);
        }
    }
}
