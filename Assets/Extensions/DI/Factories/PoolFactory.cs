﻿using System.Reflection;
using Extensions;
using UnityEngine;

namespace VG.Utilites
{
    public class PoolFactory<T> : GameObjectsPool<T> where T : Component
    {
        public PoolFactory(T prefab, int initSize = 0, int maxSize = int.MaxValue) : base(prefab, initSize, maxSize){}

        protected override T Create()
        {
            var instance = base.Create();
            
            var type = typeof(T);
            if(type.GetCustomAttribute<InjectToAttribute>() != null)
                DI.Container.InjectTo(type, instance);

            return instance;
        }
    }
}