using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputFireManager : MonoBehaviour, IGameUpdateListener
    {
        public event Action OnFired = delegate {};

        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnFired.Invoke();
        }
    }
}