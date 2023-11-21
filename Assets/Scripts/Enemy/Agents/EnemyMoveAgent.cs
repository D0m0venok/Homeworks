using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _positionInaccuracy = 0.25f;
        
        private MoveComponent _moveComponent;
        private Vector2 _destination;
        
        public bool IsReached { get; private set; }

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            if (IsReached)
                return;
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= _positionInaccuracy)
            {
                IsReached = true;
                return;
            }
            
            var direction = vector.normalized * fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }
    }
}