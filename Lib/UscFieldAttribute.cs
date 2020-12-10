using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo
{ 
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited =true)]
    public class UscFieldAttribute : Attribute
    {
        public enum Values
        { 
            None = 0,
            NotNull = 1,
            ReadOnly = 2,

            PrimaryKey = NotNull | 1024,
        }

        public UscFieldAttribute(Values value)
        {
            Value = value;
        }

        public Values Value { get; set; }

        bool _containsValue(Values value)
        {
            return (Value & value) == value;
        }

        public bool IsNotNull 
        {
            get
            {
                return _containsValue(Values.NotNull);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _containsValue(Values.ReadOnly);
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return _containsValue(Values.PrimaryKey);
            }
        }
    }
}
