using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo
{ 
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited =true)]
    public class UscFieldAttribute : Attribute
    {
        public UscFieldAttribute()
            : this(UscFieldAttrValues.None)
        {  }

        public UscFieldAttribute(UscFieldAttrValues value)
        {
            Value = value;
        }

        public UscFieldAttrValues Value { get; set; }

        bool _containsValue(UscFieldAttrValues value)
        {
            return (Value & value) == value;
        }

        public bool IsNotNull 
        {
            get
            {
                return _containsValue(UscFieldAttrValues.NotNull);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _containsValue(UscFieldAttrValues.ReadOnly);
            }
        }
    }

    [Flags]
    public enum UscFieldAttrValues
    {
        None = 0,
        NotNull = 1,
        ReadOnly = 2,
        Visible = 4,
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UscPrimaryKeyAttribute : Attribute
    {
        public UscPrimaryKeyAttribute(int order)
        {
            Order = order;
        }

        public readonly int Order;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UscForeignKeyAttribute : Attribute
    {
        public UscForeignKeyAttribute(string refObj, string[] refProps )
        {
            RefObjectName = refObj;
            RefProperties = refProps;
        }

        public readonly string RefObjectName;
        public readonly string[] RefProperties;
    }
}
