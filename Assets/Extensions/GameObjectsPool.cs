using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Extensions
{
    public abstract class GameObjectsPool<T> where T : Component
    {
        private readonly T _prefab;
        private  Transform _activeContainer;
        private  Transform _disableContainer;
        private readonly HashSet<T> _active = new();
        private readonly Queue<T> _disabled = new();
        private readonly int _maxSize;

        public int Count => _active.Count;
        public bool HasFreeObject => _active.Count < _maxSize;
        public Transform ActiveContainer
        {
            get => _activeContainer;
            set
            {
                if (_activeContainer == value)
                    return;
                
                foreach (var active in _active)
                {
                    active.transform.SetParent(value);
                }
                    
                _activeContainer = value;
            }
        }
        public Transform DisableContainer
        {
            get => _disableContainer;
            set
            {
                if (_disableContainer == value)
                    return;
                
                foreach (var disable in _disabled)
                {
                    disable.transform.SetParent(value);
                }
                
                _disableContainer = value;
            }
        }

        protected GameObjectsPool(T prefab, int initSize = 0, int maxSize = int.MaxValue)
        {
            if (prefab == null)
                throw new NullReferenceException();
            
            _prefab = prefab;

            _disableContainer = _prefab.transform.parent;
            
            _maxSize = Mathf.Clamp(maxSize, 1, int.MaxValue);
            initSize = Mathf.Clamp(initSize, 0, _maxSize);

            for (var i = 0; i < initSize; i++)
            {
                var obj = Create();
                obj.transform.SetParent(_disableContainer);
                _disabled.Enqueue(obj);
            }
        }

        public virtual bool TryGet(out T obj)
        {
            obj = default;
            if (_active.Count == _maxSize)
                return false;
            else
                obj = GetOrCreate();
            
            return true;
        }
        public virtual T Get()
        {
            T obj;

            if (_active.Count == _maxSize)
                return null;
            else
                obj = GetOrCreate();
            
            return obj;
        }
        public virtual void Put(T obj)
        {
            if(!_active.Remove(obj))
                return;
            
            _disabled.Enqueue(obj);
            obj.transform.SetParent(_disableContainer);
        }

        private T GetOrCreate()
        {
            var obj = _disabled.Count > 0 ? _disabled.Dequeue() : Create();
            
            obj.transform.SetParent(_activeContainer);
            _active.Add(obj);

            return obj;
        }
        protected virtual T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}