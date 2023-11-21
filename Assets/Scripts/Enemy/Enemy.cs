using UnityEngine;

namespace ShootEmUp
{
    
    public sealed class Enemy : Unit,
        IGameAttachListener,
        IGameDetachListener
    {
        [SerializeField] private EnemyMoveAgent _moveAgent;
        [SerializeField] private EnemyAttackAgent _attackAgent;

        public EnemyMoveAgent MoveAgent => _moveAgent;
        public EnemyAttackAgent AttackAgent => _attackAgent;
        
        public void AttachGame()
        {
            Debug.Log("Enemy enabled for test");
        }
        public void DetachGame()
        {
            Debug.Log("enemy disabled for test");
        }
    }
}