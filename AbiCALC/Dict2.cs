using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC
{
    public class Dict2<T1, T2>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Dict2()
        {
            this.Forward = new Dict2InternalDict<T1, T2>(_forward);
            this.Reverse = new Dict2InternalDict<T2, T1>(_reverse);
        }

        public class Dict2InternalDict<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Dict2InternalDict(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
            public Dictionary<T3, T4> get() => _dictionary;
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public Dict2InternalDict<T1, T2> Forward { get; private set; }
        public Dict2InternalDict<T2, T1> Reverse { get; private set; }

        public T2 this[T1 key] 
        {
            get => Keys1.Contains(key) ? _forward[key] : default;
            set => Add(key, value);
        }
        public T1 this[T2 key]
        {
            get => Keys2.Contains(key) ? _reverse[key] : default;
            set => Add(value, key);
        }
        public IEnumerable<T1> Keys1 
        {
            get => _forward.Keys;
        }
        public IEnumerable<T2> Keys2
        {
            get => _reverse.Keys;
        }
    }
}
