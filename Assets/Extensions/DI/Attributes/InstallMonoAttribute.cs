using System;

namespace VG.Utilites
{
    public enum InstallType
    {
        Instance,
        Factory,
        PoolFactory,
        SortedPoolFactory
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class InstallMonoAttribute : InjectToAttribute
    {
        public InstallMonoAttribute()
        {
            
        }
        public InstallMonoAttribute(string id)
        {
            Id = id;
        }
        public InstallMonoAttribute(InstallType type)
        {
            Type = type;
        }
        public InstallMonoAttribute(InstallType type, string id)
        {
            Id = id;
            Type = type;
        }
        
        public string Id { get; private set; }
        public InstallType Type { get; private set; }
    }
}