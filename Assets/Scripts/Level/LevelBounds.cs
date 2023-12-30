using System.Linq;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class LevelBounds
    {
        [Inject] private Transform[] _borders;
        private readonly float _minX;
        private readonly float _maxX;
        private readonly float _minY;
        private readonly float _maxY;

        public LevelBounds()
        {
            DI.Container.InjectTo(this);
            
            _minX = _borders.Min(b => b.position.x);
            _maxX = _borders.Max(b => b.position.x);
            _minY = _borders.Min(b => b.position.y);
            _maxY = _borders.Max(b => b.position.y);
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