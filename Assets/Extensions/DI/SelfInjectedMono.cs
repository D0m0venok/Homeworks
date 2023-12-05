using UnityEngine;

namespace VG.Utilites
{
    public abstract class SelfInjectedMono : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DIContainer.InjectTo(GetType(), this);
        }
    }
}