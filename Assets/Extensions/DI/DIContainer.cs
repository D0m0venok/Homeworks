using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VG.Utilites
{
    public class DIContainer
    {
        private readonly Dictionary<Type, object> _objects = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Dictionary<string, object>> _variants = new Dictionary<Type, Dictionary<string, object>>();

        public void Install(Type type, object obj, string id = null)
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
        public void Install<T>(string id = null) where T: new()
        {
            Install(typeof(T), new T(), id);
        }
        public void Install<T>(T obj, string id = null)
        {
            Install(typeof(T), obj, id);
        }
        public void Install(DIContainer container)
        {
            if (container == this)
            {
                Debug.LogWarning($"Can't add self");
                return;
            }

            foreach (var (type, obj) in container._objects)
            {
                Install(type, obj);
            }
            foreach (var (type, objects) in container._variants)
            {
                foreach (var (id, obj) in objects)
                {
                    Install(type, obj, id);
                }
            }
        }
        public void AddInstaller<T>() where T : Installer, new()
        {
            AddInstaller(new T());
        }
        public void AddInstaller(Installer installer)
        {
            installer.Install(this);
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
        public T Get<T>(string id = null)
        {
            return (T)Get(typeof(T), id);
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
        public void Remove<T>(string id = null)
        {
            Remove(typeof(T), id);
        }
        public void Remove(DIContainer container)
        {
            if (container == this)
            {
                Debug.LogWarning($"Can't add self");
                return;
            }
            
            foreach (var (type, obj) in container._objects)
            {
                Remove(type);
            }
            foreach (var (type, objects) in container._variants)
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
        public void InjectTo(Type type, object obj)
        {
            InjectToObject(type, obj, null);
        }
        public void InjectTo<T>(T obj) where T : class
        {
            InjectToObject(typeof(T), obj, null);
        }
        public IEnumerable<TypeObject> GetObjects()
        {
            var objects = _objects.Select(o => new TypeObject(o.Key, o.Value));
            var variants = _variants.SelectMany(v => v.Value.Select(o => new TypeObject(v.Key, o.Value)));
            return objects.Concat(variants);
        }

        private void InjectToObject(Type type, object obj, MonoBehaviour targetMono)
        {
            if (obj == null)
            {
                Debug.LogWarning($"Can't inject to {type}, object is null");
                return;
            }

            if (type.GetCustomAttribute<InjectToAttribute>() == null)
                return;

            if(targetMono == null)
                targetMono = obj as MonoBehaviour;
            
            foreach (var field in GetFields(type))
            {
                if (field.GetCustomAttribute<InjectToAttribute>() != null)
                {
                    InjectToObject(field.FieldType, field.GetValue(obj), targetMono);
                    continue;
                }

                var injectAttr = field.GetCustomAttribute<InjectAttribute>();
                if (injectAttr != null)
                {
                    InjectToField(field, obj, Get(field.FieldType, injectAttr.Id));
                    continue;
                }

                var attr = field.GetCustomAttribute<InjectLocalAttribute>();
                if(attr == null || targetMono == null)
                    continue;
                
                Component inject;
                if (attr.OnlyFromSelf)
                {
                    inject = targetMono.GetComponent(field.FieldType);
                }
                else
                {
                    var comps = targetMono.GetComponentsInChildren(field.FieldType, true);
                    
                    if (!string.IsNullOrEmpty(attr.Name))
                        inject = comps.FirstOrDefault(c => c.name.Equals(attr.Name));
                    else
                        inject = comps.Length == 1 ? comps.First() : comps.FirstOrDefault(c => Compare(c.name, field.Name));
                }
                
                InjectToField(field, obj, inject);
            }

            foreach (var method in GetMethods(type))
            {
                var injectAttr = method.GetCustomAttribute<InjectAttribute>();
                if (injectAttr == null)
                    continue;
                
                var parameters = method.GetParameters();
                var injects = new object[parameters.Length];
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var attr = parameter.GetCustomAttribute<InjectAttribute>();
                    var inject = Get(parameter.ParameterType, attr?.Id);
                    if (inject == null)
                    {
                        Debug.LogWarning($"Can't inject {parameter.ParameterType} to {obj} {parameter.Name}");
                        continue;
                    }

                    injects[i] = Get(parameter.ParameterType, attr?.Id);
                }

                method.Invoke(obj, injects);
            }
        }
        
        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type == null ? Enumerable.Empty<FieldInfo>() : type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Union(GetFields(type.BaseType));
        }
        private static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            return type == null ? Enumerable.Empty<MethodInfo>() : type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Union(GetMethods(type.BaseType));
        }
        private static void InjectToField(FieldInfo field, object obj, object value)
        {
            if (value == null)
            {
                Debug.LogWarning($"Can't inject {field.FieldType} to {obj} {field.Name}");
                return;
            }

            field.SetValue(obj, value);
        }
        private static bool Compare(string string1, string string2)
        {
            var chars = new[] { '_', ' ' };

            var i1 = 0; 
            var i2 = 0;
            while (i1 < string1.Length && i2 < string2.Length)
            {
                var s1 = string1[i1];
                var s2 = string2[i2];
                
                if (chars.Contains(s1))
                {
                    i1++;
                    continue;
                }

                if (chars.Contains(s2))
                {
                    i2++;
                    continue;
                }

                if (char.ToLower(s1) != char.ToLower(s2))
                    return false;

                i1++;
                i2++;
            }

            return i1 == string1.Length && i2 == string2.Length;
        }
        
        public readonly struct TypeObject
        {
            public TypeObject(Type type, object o)
            {
                Type = type;
                Object = o;
            }
        
            public Type Type { get; }
            public object Object { get; }
        }
    }
}