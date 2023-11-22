using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    
    public sealed class Enemy : Unit, IGameListenerProvider
    {
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackAgent _attackAgent;

        public EnemyMoveAgent MoveAgent => _moveAgent;
        public EnemyAttackAgent AttackAgent => _attackAgent;
        
        public IEnumerable<IGameListener> ProvideListeners()
        {
            yield return MoveAgent;
            yield return AttackAgent;
            yield return HitPointsComponent;
            yield return MoveComponent;
        }
    }
}