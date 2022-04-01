using System;

namespace MouseAttack.Item.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AssignToAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public AssignToAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

    }
}