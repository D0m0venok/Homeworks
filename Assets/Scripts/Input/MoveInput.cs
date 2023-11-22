using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveInput : MonoBehaviour, IGameFixedUpdateListener
    {
        public event Action<Vector2> OnMoved = delegate {};

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                OnMoved(new Vector2(-1, 0) * fixedDeltaTime);
            else if (Input.GetKey(KeyCode.RightArrow))
                OnMoved(new Vector2(1, 0) * fixedDeltaTime);
        }
    }
}