using System.Collections.Generic;

namespace Structurizr4Net.Core.Collections
{
    public class HashMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private TValue _nullValue;

        public new TValue this[TKey key]
        {
            get
            {
                if (!typeof(TKey).IsValueType && key == null) return _nullValue;
                return base[key];
            }
            set
            {
                if (!typeof (TKey).IsValueType && key == null)
                    _nullValue = value;
                else
                    base[key] = value;
            }
        }
    }
}
