using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoniLib.Collections
{
    public class NestedDictionary<Key1, Key2, Key3, Key4, Key5, Value> : NestedDictionary<Key1, Key2, Key3, Key4, Dictionary<Key5, Value>>
    {
        public Value this[Key1 a, Key2 b, Key3 c, Key4 d, Key5 e]
        {
            get => base[a, b, c, d][e];
            set
            {
                if (!base.ContainsKey(a, b, c, d))
                    base[a, b, c, d] = new Dictionary<Key5, Value>();

                base[a, b, c, d][e] = value;
            }
        }

        public bool ContainsKey(Key1 a, Key2 b, Key3 c, Key4 d, Key5 e) => base.ContainsKey(a, b, c, d) && base[a, b, c, d].ContainsKey(e);
    }

    public class NestedDictionary<Key1, Key2, Key3, Key4, Value> : NestedDictionary<Key1, Key2, Key3, Dictionary<Key4, Value>>
    {
        public Value this[Key1 a, Key2 b, Key3 c, Key4 d]
        {
            get => base[a, b, c][d];
            set
            {
                if (!base.ContainsKey(a, b, c))
                    base[a, b, c] = new Dictionary<Key4, Value>();

                base[a, b, c][d] = value;
            }
        }

        public bool ContainsKey(Key1 a, Key2 b, Key3 c, Key4 d) => base.ContainsKey(a, b, c) && base[a, b, c].ContainsKey(d);
    }

    public class NestedDictionary<Key1, Key2, Key3, Value> : NestedDictionary<Key1, Key2, Dictionary<Key3, Value>>
    {
        public Value this[Key1 a, Key2 b, Key3 c]
        {
            get => base[a, b][c];
            set
            {
                if (!base.ContainsKey(a, b))
                    base[a, b] = new Dictionary<Key3, Value>();

                base[a, b][c] = value;
            }
        }

        public bool ContainsKey(Key1 a, Key2 b, Key3 c) => base.ContainsKey(a, b) && base[a, b].ContainsKey(c);
    }

    public class NestedDictionary<Key1, Key2, Value>
    {
        private Dictionary<Key1, Dictionary<Key2, Value>> Dict = new Dictionary<Key1, Dictionary<Key2, Value>>();

        public Value this[Key1 a, Key2 b]
        {
            get => Dict[a][b];
            set
            {
                if (!Dict.ContainsKey(a))
                    Dict[a] = new Dictionary<Key2, Value>();

                Dict[a][b] = value;
            }
        }

        public bool ContainsKey(Key1 a, Key2 b) => Dict.ContainsKey(a) && Dict[a].ContainsKey(b);

    }
}
