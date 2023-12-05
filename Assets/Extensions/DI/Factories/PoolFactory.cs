using System.Reflection;
using ShootEmUp;
using UnityEngine;

namespace VG.Utilites
{
    public class PoolFactory<T> : GameObjectsPool<T> where T : Component, IGameStartListener
    {
        public PoolFactory(T prefab) : base(prefab)
        {
            
        }

        protected override T Create()
        {
            var instance = base.Create();
            
            var type = typeof(T);
            if(type.GetCustomAttribute<InjectToAttribute>() != null)
                DIContainer.InjectTo(type, instance);

            return instance;
        }
    }
}