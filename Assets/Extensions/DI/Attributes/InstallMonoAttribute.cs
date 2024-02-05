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
    public class InstallMonoAttribute : Attribute
    {
        public InstallMonoAttribute()
        {
        }
        public InstallMonoAttribute(bool idFromName)
        {
            IdFromName = idFromName;
        }
        public InstallMonoAttribute(InstallType type, int poolInitSize = 0, int poolMaxSize = int.MaxValue)
        {
            Type = type;
            PoolInitSize = poolInitSize;
            PoolMaxSize = poolMaxSize;
        }

        public bool IdFromName { get; }
        public InstallType Type { get; }
        public int PoolInitSize { get; }
        public int PoolMaxSize { get; }
    }
}