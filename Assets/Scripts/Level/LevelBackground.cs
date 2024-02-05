using System;
using UnityEngine;
using VG.Utilites;

namespace ShootEmUp
{
    public sealed class LevelBackground : Listener, IUpdate
    {
        private readonly Transform _myTransform;
        private readonly float _startPositionY;
        private readonly float _endPositionY;
        private readonly float _movingSpeedY;
        private readonly float _positionX;
        private readonly float _positionZ;
        
        public LevelBackground(Transform myTransform, Params @params)
        {
            _myTransform = myTransform;
            
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
            
            _startPositionY = @params.StartPositionY;
            _endPositionY = @params.EndPositionY;
            _movingSpeedY = @params.MovingSpeedY;
        }

        void IUpdate.OnEntityUpdate()
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
                _movingSpeedY * Time.deltaTime,
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