using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public abstract class GameObjectsPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly List<T> _active = new();
        private readonly Queue<T> _disabled = new();
        private readonly Transform _disabledParent;
        private readonly int _maxSize;

        public int Count => _active.Count;
        
        protected GameObjectsPool(T prefab, Transform disabledParent, int initSize = 0, int maxSize = int.MaxValue)
        {
            if (prefab == null)
                throw new NullReferenceException();

            _prefab = prefab;
            _disabledParent = disabledParent;
            _maxSize = maxSize;
            
            _maxSize = Mathf.Clamp(maxSize, 1, int.MaxValue);
            initSize = Mathf.Clamp(initSize, 0, _maxSize);
            
            for (var i = 0; i < initSize; i++)
            {
                var obj = Object.Instantiate(_prefab, _disabledParent);
                _disabled.Enqueue(obj);
            }
        }

        public virtual bool TryGet(out T obj, Transform parent = null)
        {
            obj = default;
            if (_active.Count == _maxSize)
                return false;
            else
                obj = GetOrCreate(parent);
            
            return true;
        }
        public virtual T Get(Transform parent = null)
        {
            T obj;

            if (_active.Count == _maxSize)
                return null;
            else
                obj = GetOrCreate(parent);
            
            return obj;
        }
        public virtual void Put(T obj)
        {
            if(!_active.Remove(obj))
                return;
            
            _disabled.Enqueue(obj);
            obj.transform.SetParent(_disabledParent);
        }

        private T GetOrCreate(Transform parent)
        {
            var obj = _disabled.Count > 0 ? _disabled.Dequeue() : Object.Instantiate(_prefab);
            obj.transform.SetParent(parent);
            _active.Add(obj);
            return obj;
        }
    }
}