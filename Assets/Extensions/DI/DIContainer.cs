using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace VG.Utilites
{
    public static class DIContainer
    {
        // public static void Install<T>(string id = null) where T : new()
        // {
        //     Install(new T(), id);
        // }
        // public static void Install(Type type, object obj, string id = null)
        // {
        //     _collection.Add(type, obj, id);
        // }
        // public static void Install<T>(T obj, string id = null)
        // {
        //     Install(typeof(T), obj, id);
        // }
        // public static void Install<T>(Func<T> creator, string id = null)
        // {
        //     Install(typeof(T), creator(), id);
        // }
        public static void Install(DICollection collection)
        {
            _collection.Add(collection);
        }
        // public static bool IsInstalled(Type type, string id = null)
        // {
        //     return _collection.Contains(type, id);
        // }
        // public static bool IsInstalled<T>(string id = null)
        // {
        //     return IsInstalled(typeof(T));
        // }
        public static object Get(Type type, string id = null)
        {
            return _collection.Get(type, id);
        }
        public static T Get<T>(string id = null)
        {
            return (T)Get(typeof(T), id);
        }
        // public static void Remove(Type type, string id = null)
        // {
        //     _collection.Remove(type, id);
        // }
        // public static void Remove<T>(string id = null)
        // {
        //     Remove(typeof(T), id);
        // }
        public static void Remove(DICollection collection)
        {
            _collection.Remove(collection);
        }
        public static void InjectTo(Type type, object obj)
        {
            InjectToObject(type, obj, null);
        }
        public static void InjectTo<T>(T obj) where T : class
        {
            InjectTo(typeof(T), obj);
        }

        private static void InjectToObject(Type type, object obj, MonoBehaviour targetMono)
        {
            if (obj == null)
                throw new NullReferenceException();
            
            if(targetMono == null)
                targetMono = obj as MonoBehaviour;
            
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (field.GetCustomAttribute<InjectToAttribute>() != null)
                {
                    InjectToObject(field.FieldType, field.GetValue(obj), targetMono);
                    continue;
                }

                var injectAttr = field.GetCustomAttribute<InjectAttribute>();
                if (injectAttr != null)
                {
                    InjectToField(field, obj, Get(field.FieldType, injectAttr.Id), Color.yellow);
                    continue;
                }

                var attr = field.GetCustomAttribute<InjectLocalAttribute>();
                if(attr == null || targetMono == null)
                    continue;
                
                var comps = targetMono.GetComponentsInChildren(field.FieldType, true);
                Component inject;
                if (!string.IsNullOrEmpty(attr.Name))
                    inject = comps.FirstOrDefault(c => c.name.Equals(attr.Name));
                else
                    inject = comps.Length == 1 ? comps.First() : comps.FirstOrDefault(c => Compare(c.name, field.Name));
                
                InjectToField(field, obj, inject, Color.cyan);
            }
        }
        private static void InjectToField(FieldInfo field, object obj, object value, Color color)
        {
            if (value == null)
            {
                Debug.LogWarning($"Can't injected {field.FieldType} to {obj} {field.Name}");
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

            return true;
        }
        
        private static readonly DICollection _collection = new DICollection();
    }
}