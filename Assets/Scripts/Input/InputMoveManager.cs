using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveManager : MonoBehaviour
    {
        public event Action<Vector2> OnMoved = delegate {};

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                OnMoved(new Vector2(-1, 0) * Time.fixedDeltaTime);
            else if (Input.GetKey(KeyCode.RightArrow))
                OnMoved(new Vector2(1, 0) * Time.fixedDeltaTime);
        }
    }
}