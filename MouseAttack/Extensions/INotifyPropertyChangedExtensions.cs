using System;
using System.ComponentModel;
using System.Reflection;

namespace MouseAttack.Extensions
{
    public static class INotifyPropertyChangedExtensions
    {
        public static void Listen(this INotifyPropertyChanged source, string propertyName, Action onChanged)
        {
            source.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                    onChanged();
            };
        }

        public static void Bind(this INotifyPropertyChanged source,
            string sourcePropertyName,
            object target,
            string targetPropertyName,
            Func<object, object> propertyConvertor = null,
            bool setImmediate = true)
        {
            void SetProperty()
            {
                // Get Source's Property value
                object sourcePropertyValue = source
                    .GetType()
                    .GetProperty(sourcePropertyName)
                    .GetValue(source);

                // Set Target's Property value
                PropertyInfo targetPropertyInfo = target
                    .GetType()
                    .GetProperty(targetPropertyName);

                targetPropertyInfo
                   .SetValue(target, propertyConvertor != null ?
                   propertyConvertor(sourcePropertyValue) :
                   Convert.ChangeType(sourcePropertyValue, targetPropertyInfo.PropertyType));
            }

            if (setImmediate)
                SetProperty();

            source.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != sourcePropertyName)
                    return;
                SetProperty();
            };
        }
    }

}
