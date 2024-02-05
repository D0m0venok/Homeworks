using System.Linq;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;
        private readonly float _maxY;
        
        public LevelBounds(Transform[] borders)
        {
            _minX = borders.Min(b => b.position.x);
            _maxX = borders.Max(b => b.position.x);
            _minY = borders.Min(b => b.position.y);
            _maxY = borders.Max(b => b.position.y);
        }

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _minX && positionX < _maxX 
                && positionY > _minY && positionY < _maxY;
        }
    }
}