using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class EnemyPositions
    {
        [Inject] private Transform[] _spawnPositions;
        [Inject] private Transform[] _attackPositions;
        
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