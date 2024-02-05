using UnityEngine;

namespace VG.Utilites
{
    public abstract class ScriptableInstaller : ScriptableObject
    {
        public abstract void Install(DIContainer container);
    }
}