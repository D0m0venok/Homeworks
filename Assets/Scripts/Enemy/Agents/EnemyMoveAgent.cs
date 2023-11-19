using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private float _positionInaccuracy = 0.25f;
        
        private MoveComponent _moveComponent;
        private Vector2 _destination;
        
        public bool IsReached { get; private set; }

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        private void FixedUpdate()
        {
            if (IsReached)
                return;
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= _positionInaccuracy)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}