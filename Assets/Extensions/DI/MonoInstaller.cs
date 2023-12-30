using UnityEngine;

namespace VG.Utilites
{
    [AddComponentMenu("DI/Mono Installer")]
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Install(DIContainer container);
    }
}