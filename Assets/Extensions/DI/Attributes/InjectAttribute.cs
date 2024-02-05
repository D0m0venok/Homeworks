using System;

namespace VG.Utilites
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {
            
        }
        public InjectAttribute(string id)
        {
            Id = id;
        }
        
        public string Id { get; }
    }
}