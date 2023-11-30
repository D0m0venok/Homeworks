using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _additionalGameListeners;
        
        [Inject]
        private void Construct(IGameListener[] listeners)
        {
            var manager = GetComponent<GameManager>();
            foreach (var listener in listeners)
            {
                manager.AddListener(listener);   
            }
            
            listeners = GetComponentsInChildren<IGameListener>();

            foreach (var listener in listeners)
            {
                manager.AddListener(listener);
            }
            foreach (var listener in _additionalGameListeners)
            {
                if(listener is IGameListener gameListener)
                    manager.AddListener(gameListener);
            }
        }
    }
}