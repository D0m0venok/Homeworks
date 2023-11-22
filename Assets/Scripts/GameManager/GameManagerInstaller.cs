using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeReference] 
        private List<MonoBehaviour> _additionalGameListeners = new();
        
        private void Awake()
        {
            var manager = GetComponent<GameManager>();
            var listeners = GetComponentsInChildren<IGameListener>();

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