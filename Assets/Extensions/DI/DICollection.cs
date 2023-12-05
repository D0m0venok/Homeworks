using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

namespace VG.Utilites
{
    public class DICollection
    {
        private readonly Dictionary<Type, object> _objects = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Dictionary<string, object>> _variants = new Dictionary<Type, Dictionary<string, object>>();

        public void Add(Type type, object obj, string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                if (!_objects.TryAdd(type, obj))
                    Debug.LogWarning($"DI collection already has a type {type}");
                
                return;
            }

            if (!_variants.ContainsKey(type))
                _variants.Add(type, new Dictionary<string, object>());

            var objects = _variants[type];
            if (!objects.TryAdd(id, obj))
                Debug.LogWarning($"DI collection already has a type {type} with id {id}");
        }
        public void Add(DICollection collection)
        {
            foreach (var (type, obj) in collection._objects)
            {
                Add(type, obj);
            }
            foreach (var (type, objects) in collection._variants)
            {
                foreach (var (id, obj) in objects)
                {
                    Add(type, obj, id);
                }
            }
        }
        public object Get(Type type, string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                _objects.TryGetValue(type, out var o);
                return o;
            }

            if(!_variants.TryGetValue(type, out var objects))
                return null;

            objects.TryGetValue(id, out var obj);
            return obj;
        }
        public void Remove(Type type, string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                _objects.Remove(type);
                return;
            }

            if (_variants.TryGetValue(type, out var objects))
                objects.Remove(id);
        }
        public void Remove(DICollection collection)
        {
            foreach (var (type, obj) in collection._objects)
            {
                Remove(type);
            }
            foreach (var (type, objects) in collection._variants)
            {
                foreach (var id in objects.Keys)
                {
                    Remove(type, id);
                }
            }
        }
        public bool Contains(Type type, string id = null)
        {
            if(string.IsNullOrEmpty(id))
                return _objects.ContainsKey(type);
            
            return _variants.TryGetValue(type, out var objects) && objects.ContainsKey(id);
        }
    }
}