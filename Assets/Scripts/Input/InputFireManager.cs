using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputFireManager : MonoBehaviour
    {
        public event Action OnFired = delegate {};

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnFired.Invoke();
        }
    }
}