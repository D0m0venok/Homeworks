using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameStartListener, IGameFixedUpdateListener
    {
        [SerializeField] private Params _params;
        
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;
        
        public void OnStartGame()
        {
            _startPositionY = _params.MStartPositionY;
            _endPositionY = _params.MEndPositionY;
            _movingSpeedY = _params.MMovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate(float fixedDeltaTime)
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
                _movingSpeedY * fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] private float _startPositionY;
            [SerializeField] private float _endPositionY;
            [SerializeField] private float _movingSpeedY;
            
            public float MStartPositionY => _startPositionY;
            public float MEndPositionY => _endPositionY;
            public float MMovingSpeedY => _movingSpeedY;
        }
    }
}