using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VG.Utilites
{
    public class Listeners<TListener> where TListener: IListener
    {
        private readonly Dictionary<Type, HashSet<TListener>> _dictionary = new Dictionary<Type, HashSet<TListener>>();
        private readonly Dictionary<Type, List<TListener>> _add = new Dictionary<Type, List<TListener>>();
        private readonly Dictionary<Type, List<TListener>> _remove = new Dictionary<Type, List<TListener>>();

        public IReadOnlyDictionary<Type, int> Count { get { return _dictionary.ToDictionary(pair => pair.Key, pair => pair.Value.Count); } }

        public void Add(TListener listener)
        {
            var tListenerType = typeof(TListener);
            foreach (var type in listener.GetType().GetInterfaces())
            {
                if(type != tListenerType && tListenerType.IsAssignableFrom(type))
                    Add(type, listener);
            }
        }
        public void Add(IEnumerable<TListener> listeners)
        {
            foreach (var listener in listeners)
            {
                Add(listener);
            }
        }
        public void Remove(TListener listener)
        {
            foreach (var type in listener.GetType().GetInterfaces())
            {
                Remove(type, listener);
            }
        }
        public void Remove(IEnumerable<TListener> listeners)
        {
            foreach (var listener in listeners)
            {
                Remove(listener);
            }
        }
        public void ForEach<T>(Action<T> action) where T : TListener
        {
            if(action == null)
                return;
            
            var type = typeof(T);
            if (!_dictionary.ContainsKey(type))
                return;

            _busy = type;
            foreach (var listener in _dictionary[type])
            {
                action.Invoke((T) listener);
            }
            _busy = null;
                
            var set = _add[type];
            foreach (var listener in set)
            {
                _dictionary[type].Add(listener);
            }
            set.Clear();
            
            set = _remove[type];
            foreach (var listener in set)
            {
                _dictionary[type].Remove(listener);
            }
            set.Clear();
        }

        private void Add(Type type, TListener listener)
        {
            if (!_dictionary.ContainsKey(type))
            {
                _dictionary.Add(type, new HashSet<TListener>());
                _add.Add(type, new List<TListener>());
                _remove.Add(type, new List<TListener>());
            }

            if (_busy == type)
                _add[type].Add(listener);
            else
                _dictionary[type].Add(listener);
        }
        private void Remove(Type type, TListener listener)
        {
            if(_busy == type && _dictionary.ContainsKey(type))
                _remove[type].Add(listener);
            else
                _dictionary[type].Remove(listener);
        }

        private Type _busy;
    }
}