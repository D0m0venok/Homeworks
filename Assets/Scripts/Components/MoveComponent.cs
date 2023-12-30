using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : RigidbodyStateController
    {
        private readonly float _speed;

        public MoveComponent(Rigidbody2D rigidbody2D, float speed)
        {
            _speed = speed;
            Init(rigidbody2D);
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}