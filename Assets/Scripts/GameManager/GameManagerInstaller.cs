using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            var listeners = GetComponentsInChildren<IGameListener>();

            foreach (var listener in listeners)
            {
                manager.AddListener(listener);
            }
        }
    }
}