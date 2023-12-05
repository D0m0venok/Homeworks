using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            var roots = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var root in roots)
            {
                var listener = root.GetComponent<IGameListener>();

                if (listener != null)
                    manager.AddListener(listener);
            }
        }
    }
}