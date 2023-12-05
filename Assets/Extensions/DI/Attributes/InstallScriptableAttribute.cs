using System;

namespace VG.Utilites
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InstallScriptableAttribute : Attribute
    {
        public InstallScriptableAttribute()
        {
            
        }
        public InstallScriptableAttribute(string id)
        {
            Id = id;
        }
        
        public string Id { get; private set; }
    }
}