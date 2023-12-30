using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class LevelBackground : IFixedUpdate
    {
        [Inject] private Transform _myTransform;
        [Inject] private Params _params;
        private readonly float _startPositionY;
        private readonly float _endPositionY;
        private readonly float _movingSpeedY;
        private readonly float _positionX;
        private readonly float _positionZ;

        public LevelBackground()
        {
            ListenersManager.Add(this);
            
            DI.Container.InjectTo(this);
            
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
            
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
        }

        void IFixedUpdate.OnEntityFixedUpdate()
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] private float _startPositionY;
            [SerializeField] private float _endPositionY;
            [SerializeField] private float _movingSpeedY;
            
            public float StartPositionY => _startPositionY;
            public float EndPositionY => _endPositionY;
            public float MovingSpeedY => _movingSpeedY;
        }
    }
}