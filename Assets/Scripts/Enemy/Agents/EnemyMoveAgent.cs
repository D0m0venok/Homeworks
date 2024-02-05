using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : EntityComponent, IFixedUpdate
    {
        private readonly float _positionInaccuracy;
        private Vector2 _destination;

        public EnemyMoveAgent(float positionInaccuracy)
        {
            _positionInaccuracy = positionInaccuracy;
        }
        
        public bool IsReached { get; private set; }
        
        void IFixedUpdate.OnEntityFixedUpdate()
        {
            if (IsReached)
                return;
            
            var vector = _destination - (Vector2) Entity.transform.position;
            if (vector.magnitude <= _positionInaccuracy)
            {
                IsReached = true;
                return;
            }
            
            var direction = vector.normalized * Time.fixedDeltaTime;
            Entity.Get<MoveComponent>().MoveByRigidbodyVelocity(direction);
        }
        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }
    }
}