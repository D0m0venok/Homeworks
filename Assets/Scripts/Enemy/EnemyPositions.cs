using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class EnemyPositions
    {
        private readonly Transform[] _spawnPositions; 
        private readonly Transform[] _attackPositions;

        public EnemyPositions(Transform[] spawnPositions, Transform[] attackPositions)
        {
            _spawnPositions = spawnPositions;
            _attackPositions = attackPositions;
        }
        
        public Transform RandomSpawnPosition()
        {
            return RandomTransform(_spawnPositions);
        }
        public Transform RandomAttackPosition()
        {
            return RandomTransform(_attackPositions);
        }

        private static Transform RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}