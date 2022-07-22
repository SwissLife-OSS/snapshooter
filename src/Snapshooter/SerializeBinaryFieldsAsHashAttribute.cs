
using System;

namespace Snapshooter
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class SerializeBinaryFieldsAsHashAttribute : Attribute
    {
        public bool Value
        {
            get; private set;
        }

        public SerializeBinaryFieldsAsHashAttribute(bool value)
        {
            Value = value;
        }
    }
}
