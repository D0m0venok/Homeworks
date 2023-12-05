using System;

namespace VG.Utilites
{
    public abstract class Installer : IInstaller
    {
        public DICollection Process()
        {
            OnInstall();
            return _collection;
        }
        
        protected void Install<T>(string id = null) where T : new()
        {
            Install(new T(), id);
        }
        protected void Install(Type type, object obj, string id = null)
        {
            _collection.Add(type, obj, id);
        }
        protected void Install<T>(T obj, string id = null)
        {
            Install(typeof(T), obj, id);
        }
        protected void Install<T>(Func<T> creator, string id = null)
        {
            Install(typeof(T), creator(), id);
        }
        protected abstract void OnInstall();
        
        private readonly DICollection _collection = new DICollection();
    }
}