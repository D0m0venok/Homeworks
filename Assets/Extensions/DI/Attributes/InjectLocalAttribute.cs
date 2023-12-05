using System;

namespace VG.Utilites
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectLocalAttribute : Attribute
    {
        public InjectLocalAttribute()
        {
            
        }
        public InjectLocalAttribute(string name)
        {
            Name = name;
        }
        
        public string Name { get; }
    }
}