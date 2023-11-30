using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveInput
    {
        public event Action<Vector2> OnMoved;
    }
}