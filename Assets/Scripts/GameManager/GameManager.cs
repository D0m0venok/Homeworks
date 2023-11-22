using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public enum GameState
    {
        OFF = 0,
        PLAYING = 1,
        PAUSED = 2,
        FINISHED = 3
    }
    
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameState _state;

        private readonly HashSet<IGameListener> _listeners = new();
        private readonly List<IGameStartListener> _startListeners = new();
        private readonly List<IGameFinishListener> _finishListeners = new();
        private readonly List<IGamePauseListener> _pauseListeners = new();
        private readonly List<IGameResumeListener> _resumeListeners = new();
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        
        public GameState State => _state;
        
        private void Update()
        {
            if (_state != GameState.PLAYING)
                return;

            var deltaTime = Time.deltaTime;
            var cache = new List<IGameUpdateListener>(_updateListeners);
            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var listener = cache[i];
                listener.OnUpdate(deltaTime);
            }
        }
        private void FixedUpdate()
        {
            if (_state != GameState.PLAYING)
                return;

            var deltaTime = Time.fixedDeltaTime;
            var cache = new List<IGameFixedUpdateListener>(_fixedUpdateListeners);
            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var listener = cache[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        public void AddListeners(IEnumerable<IGameListener> gameListeners)
        {
            if(gameListeners == null)
                return;

            foreach (var listener in gameListeners)
            {
                AddListener(listener);
            }
        }
        public void RemoveListeners(IEnumerable<IGameListener> gameListeners)
        {
            if(gameListeners == null)
                return;

            foreach (var listener in gameListeners)
            {
                RemoveListener(listener);
            }
        }
        public void AddListener(IGameListener listener)
        {
            if (listener == null || _listeners.Contains(listener))
                return;

            _listeners.Add(listener);

            if (listener is IGameAttachListener attachElement)
                attachElement.Attach();
            
            if (listener is IGameStartListener startListener)
                _startListeners.Add(startListener);
            
            if (listener is IGameFinishListener finishListener)
                _finishListeners.Add(finishListener);

            if (listener is IGamePauseListener pauseListener)
                _pauseListeners.Add(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _resumeListeners.Add(resumeListener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Add(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _fixedUpdateListeners.Add(fixedUpdateListener);
        }
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null || !_listeners.Contains(listener))
                return;
            
            _listeners.Remove(listener);
            
            if (listener is IGameDetachListener attachElement)
                attachElement.Detach();
            
            if (listener is IGameStartListener startListener)
                _startListeners.Remove(startListener);
            
            if (listener is IGameFinishListener finishListener)
                _finishListeners.Remove(finishListener);

            if (listener is IGamePauseListener pauseListener)
                _pauseListeners.Remove(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _resumeListeners.Remove(resumeListener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Remove(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _fixedUpdateListeners.Remove(fixedUpdateListener);
        }
        public void StartGame()
        {
            var cache = GetCashListeners(_startListeners);
            foreach (var listener in cache)
            {
                listener.OnStartGame();
            }
            _state = GameState.PLAYING;
        }
        public void PauseGame()
        {
            var cache = GetCashListeners(_pauseListeners);
            foreach (var listener in cache)
            {
                listener.OnPauseGame();
            }

            _state = GameState.PAUSED;
        }
        public void ResumeGame()
        {
            var cache = GetCashListeners(_resumeListeners);
            foreach (var listener in cache)
            {
                listener.OnResumeGame();
            }

            _state = GameState.PLAYING;
        }

        public void FinishGame()
        {
            var cache = GetCashListeners(_finishListeners);
            foreach (var listener in cache)
            {
                    listener.OnFinishGame();
            }

            _state = GameState.FINISHED;
        }

        private List<T> GetCashListeners<T>(List<T> collections) where T: IGameListener
        {
            return new List<T>(collections);
        }
    }
}