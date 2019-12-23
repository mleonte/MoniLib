using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace MoniLib.Data
{
    public class JsonDataCollection<TKey, TValue>
    {
        private string FilePath { get; set; }
        private Dictionary<TKey, TValue> objects;
        private Dictionary<TKey, TValue> Objects
        {
            get
            {
                if (objects == null) Refresh();
                return objects;
            }
        }

        private static JsonSerializerSettings Settings =
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        public JsonDataCollection(string filePath)
        {
            FilePath = filePath;
        }

        public void Refresh()
        {
            try
            {
                objects = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(File.ReadAllText(FilePath));
                if (objects == null)
                    objects = new Dictionary<TKey, TValue>();
            }
            catch (FileNotFoundException)
            {
                File.CreateText(FilePath);
                objects = new Dictionary<TKey, TValue>();
            }
            catch (JsonException)
            {
                objects = new Dictionary<TKey, TValue>();
            }
        }

        public List<TValue> All => Objects.Values.ToList();

        public TValue this[TKey key]
        {
            get => Objects[key];
            set { Objects[key] = value; }
        }

        public void Add(Func<TValue, TKey> keySelector, List<TValue> objs)
        {
            for (int i = 0; i < objs.Count; i++)
                Objects[keySelector(objs[i])] = objs[i];
        }

        public void Commit()
        {
            if (objects != null)
            {
                var json = JsonConvert.SerializeObject(objects, Settings);
                File.WriteAllText(FilePath, json);
            }
        }

        public void Clear() => objects.Clear();

        public void Remove(TKey key) => Objects.Remove(key);
    }
}
