using UnityEngine;

namespace VG.Utilites
{
    public abstract class SelfInjectedMono : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DI.Container.InjectTo(GetType(), this);
        }
    }
}