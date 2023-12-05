using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
    public abstract class GameObjectsPool<T> where T : Component, IGameStartListener
    {
        private readonly T _prefab;
        private readonly HashSet<T> _active = new();
        private readonly Queue<T> _disabled = new();
        private readonly Transform _container;
        private readonly int _maxSize;

        public int Count => _active.Count;
        public bool HasFreeObject => _active.Count < _maxSize;

        protected GameObjectsPool(T prefab, Transform container = null, int initSize = 0, int maxSize = int.MaxValue)
        {
            if (prefab == null)
                throw new NullReferenceException();
            
            _prefab = prefab;
            _container = container;
            
            _maxSize = Mathf.Clamp(maxSize, 1, int.MaxValue);
            initSize = Mathf.Clamp(initSize, 0, _maxSize);
            
            for (var i = 0; i < initSize; i++)
            {
                var obj = Create();
                obj.transform.SetParent(_container);
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
            obj.transform.SetParent(_container);
        }

        private T GetOrCreate(Transform parent)
        {
            var obj = _disabled.Count > 0 ? _disabled.Dequeue() : Create();
            
            obj.transform.SetParent(parent);
            _active.Add(obj);

            return obj;
        }
        protected virtual T Create()
        {
            return Object.Instantiate(_prefab);
        }
    }
}