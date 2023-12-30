using System;

namespace VG.Utilites
{
    public enum InstallType
    {
        Instance,
        Factory,
        PoolFactory,
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InstallMonoAttribute : InjectToAttribute
    {
        public InstallMonoAttribute()
        {
            
        }
        public InstallMonoAttribute(InstallType type, int poolInitSize = 0, int poolMaxSize = int.MaxValue)
        {
            Type = type;
            PoolInitSize = poolInitSize;
            PoolMaxSize = poolMaxSize;
        }
        
        public bool IdFromName;
        public InstallType Type { get; }
        public int PoolInitSize { get; }
        public int PoolMaxSize { get; }
    }
}