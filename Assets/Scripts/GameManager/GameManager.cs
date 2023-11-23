using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly HashSet<IGameListener> _cacheListeners = new();
        private readonly DictionaryKeyTypeValueHashSet<IGameListener> _listeners = new();
        
        public GameState State => _state;
        
        private void Update()
        {
            if (!IsPlaying())
                return;

            var deltaTime = Time.deltaTime;

            _listeners.CacheForEach<IGameUpdateListener>(listener => listener.OnUpdate(deltaTime), () => !IsPlaying());
        }
        private void FixedUpdate()
        {
            if (!IsPlaying())
                return;

            var deltaTime = Time.fixedDeltaTime;

            _listeners.CacheForEach<IGameFixedUpdateListener>(listener => listener.OnFixedUpdate(deltaTime), () => !IsPlaying());
        }
        private void LateUpdate()
        {
            if (!IsPlaying())
                return;

            var deltaTime = Time.deltaTime;

            _listeners.CacheForEach<IGameLateUpdateListener>(listener => listener.OnLateUpdate(deltaTime), () => !IsPlaying());
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
            if (listener == null)
                return;

            if (_cacheListeners.Contains(listener))
                throw new Exception($"Key {listener} has already been added");

            _cacheListeners.Add(listener);

            if (listener is IGameAttachListener attachElement)
                attachElement.Attach();
            
            if (listener is IGameStartListener startListener)
                _listeners.Add(startListener);
            
            if (listener is IGameFinishListener finishListener)
                _listeners.Add(finishListener);

            if (listener is IGamePauseListener pauseListener)
                _listeners.Add(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _listeners.Add(resumeListener);

            if (listener is IGameUpdateListener updateListener)
                _listeners.Add(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _listeners.Add(fixedUpdateListener);
            
            if (listener is IGameLateUpdateListener lateUpdateListener)
                _listeners.Add(lateUpdateListener);
        }
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null || !_cacheListeners.Contains(listener))
                return;

            _cacheListeners.Remove(listener);
            
            if (listener is IGameDetachListener attachElement)
                attachElement.Detach();
            
            if (listener is IGameStartListener startListener)
                _listeners.Remove(startListener);
            
            if (listener is IGameFinishListener finishListener)
                _listeners.Remove(finishListener);

            if (listener is IGamePauseListener pauseListener)
                _listeners.Remove(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _listeners.Remove(resumeListener);

            if (listener is IGameUpdateListener updateListener)
                _listeners.Remove(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _listeners.Remove(fixedUpdateListener);
        }
        public void StartGame()
        {
            _listeners.CacheForEach<IGameStartListener>(listener => listener.OnStartGame());
            _state = GameState.PLAYING;
        }
        public void PauseGame()
        {
            _listeners.CacheForEach<IGamePauseListener>(listener => listener.OnPauseGame());
            _state = GameState.PAUSED;
        }
        public void ResumeGame()
        {
            _listeners.CacheForEach<IGameResumeListener>(listener => listener.OnResumeGame());

            _state = GameState.PLAYING;
        }
        public void FinishGame()
        {
            _listeners.CacheForEach<IGameFinishListener>(listener => listener.OnFinishGame());
            _state = GameState.FINISHED;
        }
        
        private bool IsPlaying()
        {
            return _state == GameState.PLAYING;
        } 
    }

    internal class DictionaryKeyTypeValueHashSet<TType>
    {
        private readonly Dictionary<Type, HashSet<TType>> _dictionary = new();

        public void Add<T>(T value) where T : TType
        {
            var type = typeof(T);
            if(!_dictionary.ContainsKey(type))
                _dictionary.Add(type, new HashSet<TType>());

            _dictionary[type].Add(value);
        }
        public void Remove<T>(T value) where T : TType
        {
            var type = typeof(T);
            if(!_dictionary.ContainsKey(type))
                return;

            
            _dictionary[type].Remove(value);
        }
        public bool ContainsKey(Type key)
        {
            return _dictionary.ContainsKey(key);
        }
        public bool ContainsValue<T>(T value)  where T : TType
        {
            var type = typeof(T);
            if (!ContainsKey(type))
                return false;
            
            return _dictionary[type].Contains(value);
        }
        public void ForEach<T>(Action<T> action, Func<bool> canBeCanceled = null) where T : TType
        {
            if(action == null)
                return;
            
            var type = typeof(T);
            if(!_dictionary.ContainsKey(type))
                return;

            foreach (var value in _dictionary[type])
            {
                if(canBeCanceled != null && canBeCanceled())
                    break;
                
                action.Invoke((T)value);
            }
        }
        public void CacheForEach<T>(Action<T> action, Func<bool> canBeCanceled = null) where T : TType
        {
            if(action == null)
                return;
            
            var type = typeof(T);
            if(!_dictionary.ContainsKey(type))
                return;

            var cache = _dictionary[type].ToArray().AsSpan();
            foreach (var value in cache)
            {
                if (canBeCanceled != null && canBeCanceled())
                {
                    Debug.Log("Breack");
                    break;
                }
                
                action.Invoke((T)value);
            }
        }
    } 
}