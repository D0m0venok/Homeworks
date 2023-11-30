using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : RigidbodyStateController
    {
        private readonly float _speed;

        public MoveComponent(Rigidbody2D rigidbody2D, float speed) : base(rigidbody2D)
        {
            _speed = speed;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}