using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCore5Demo
{
    public abstract class UscKeyValueSet
    {
        public UscKeyValueSet() { }
        public UscKeyValueSet(params object[] keyValues) => InnerSetValue(keyValues);

        protected abstract void InnerSetValue(params object[] keyValues);

        public abstract object[] ToKeyValues();        
    }

    public class UscKeyValueSet<T> : UscKeyValueSet
    {
        public UscKeyValueSet()
            : base() { }

        public UscKeyValueSet(T value)
            : this() 
        {
            Value = value;
        }

        public T Value { get; set; }

        protected override void InnerSetValue(params object[] keyValues) 
        {
            if (keyValues == null && typeof(T).IsValueType) throw new ArgumentNullException(nameof(keyValues));
            if (keyValues.Length != 1) throw new ArgumentException("keyValues count not match.", nameof(keyValues));
            Value = (T)keyValues[0];
        }

        public override object[] ToKeyValues()
        {
            return new object[] { Value };
        }

        public static implicit operator T(UscKeyValueSet<T> value) => value.Value;
        public static implicit operator UscKeyValueSet<T>(T value) => new UscKeyValueSet<T>(value);

        public override string ToString() => $"{Value}";
    }

    public abstract class UscCompositeKeyValueSet : UscKeyValueSet
    {
        public UscCompositeKeyValueSet()
            : base() 
        {
            string[] keys = InnerGetKeys();
            valueSet = new Dictionary<string, object>(keys.Length);
            foreach (string key in keys)
                valueSet.Add(key, null);
        }

        readonly Dictionary<string, object> valueSet;


        protected abstract string[] InnerGetKeys();

        protected override void InnerSetValue(params object[] keyValues)
        {
            string[] keys = InnerGetKeys();
            if (keyValues == null)
            {
                for (int i = 0; i < keys.Length; i++)
                    valueSet[keys[i]] = null;
            }
            else
            {
                if (keyValues.Length != keys.Length)
                    throw new ArgumentException("keyValues count not match.", nameof(keyValues));
                for (int i = 0; i < keys.Length; i++)
                    valueSet[keys[i]] = keyValues[i];
            }
        }

        protected void InnerSetKeyValue(string key, object value)
        {
            valueSet[key] = value;
        }
        
        public override object[] ToKeyValues()
        {
            object[] values = new object[valueSet.Count];
            valueSet.Values.CopyTo(values, 0);
            return values;
        }
        
        public override string ToString()
        {
            return $"{valueSet}";
        }
    }
}
