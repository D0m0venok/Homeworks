using UnityEngine;

namespace ShootEmUp
{
    
    public sealed class Enemy : Unit
    {
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackAgent _attackAgent;

        public EnemyMoveAgent MoveAgent => _moveAgent;
        public EnemyAttackAgent AttackAgent => _attackAgent;
    }
}