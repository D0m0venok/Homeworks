using System.Linq;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        
        public void Construct(Transform[] borders)
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