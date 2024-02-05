using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VG.Utilites
{
    public class Factory<T> where T : Component
    {
        public Factory(T prefab)
        {
            _prefab = prefab;
        }

        public T Instantiate()
        {
            var instance = Object.Instantiate(_prefab);
            
            DI.Container.InjectTo(instance);

            return instance;
        }

        private readonly T _prefab;
    }
}