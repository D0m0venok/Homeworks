using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VG.Utilites;

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
        private GameState _state;
        //private readonly HashSet<IGameListener> _cacheListeners = new();
        //private readonly Listeners<IGameListener> _listeners = new();

        public GameState State => _state;

        // private void Update()
        // {
        //     if (!IsPlaying())
        //         return;
        //
        //     var deltaTime = Time.deltaTime;
        //     _listeners.CacheForEach<IGameUpdateListener>(listener => listener.OnUpdate(deltaTime));
        // }
        // private void FixedUpdate()
        // {
        //     if (!IsPlaying())
        //         return;
        //
        //     var deltaTime = Time.fixedDeltaTime;
        //
        //     _listeners.CacheForEach<IGameFixedUpdateListener>(listener => listener.OnFixedUpdate(deltaTime));
        // }
        // private void LateUpdate()
        // {
        //     if (!IsPlaying())
        //         return;
        //
        //     var deltaTime = Time.deltaTime;
        //
        //     _listeners.CacheForEach<IGameLateUpdateListener>(listener => listener.OnLateUpdate(deltaTime));
        // }

        // public void AddListeners(IEnumerable<IGameListener> gameListeners)
        // {
        //     if (gameListeners == null)
        //         return;
        //
        //     foreach (var listener in gameListeners)
        //     {
        //         AddListener(listener);
        //     }
        // }
        // public void RemoveListeners(IEnumerable<IGameListener> gameListeners)
        // {
        //     if (gameListeners == null)
        //         return;
        //
        //     foreach (var listener in gameListeners)
        //     {
        //         RemoveListener(listener);
        //     }
        // }
        // public void AddListener(IGameListener listener)
        // {
        //     if (listener == null)
        //         return;
        //
        //     if (_cacheListeners.Contains(listener))
        //         throw new Exception($"Key {listener} has already been added");
        //
        //     _cacheListeners.Add(listener);
        //
        //     if (listener is IGameAttachListener attachElement)
        //         attachElement.Attach();
        //
        //     if (listener is IGameStartListener startListener)
        //         _listeners.Add(startListener);
        //
        //     if (listener is IGameFinishListener finishListener)
        //         _listeners.Add(finishListener);
        //
        //     if (listener is IGamePauseListener pauseListener)
        //         _listeners.Add(pauseListener);
        //
        //     if (listener is IGameResumeListener resumeListener)
        //         _listeners.Add(resumeListener);
        //
        //     if (listener is IGameUpdateListener updateListener)
        //         _listeners.Add(updateListener);
        //
        //     if (listener is IGameFixedUpdateListener fixedUpdateListener)
        //         _listeners.Add(fixedUpdateListener);
        //
        //     if (listener is IGameLateUpdateListener lateUpdateListener)
        //         _listeners.Add(lateUpdateListener);
        // }
        // public void RemoveListener(IGameListener listener)
        // {
        //     if (listener == null || !_cacheListeners.Contains(listener))
        //         return;
        //
        //     _cacheListeners.Remove(listener);
        //
        //     if (listener is IGameDetachListener attachElement)
        //         attachElement.Detach();
        //
        //     if (listener is IGameStartListener startListener)
        //         _listeners.Remove(startListener);
        //
        //     if (listener is IGameFinishListener finishListener)
        //         _listeners.Remove(finishListener);
        //
        //     if (listener is IGamePauseListener pauseListener)
        //         _listeners.Remove(pauseListener);
        //
        //     if (listener is IGameResumeListener resumeListener)
        //         _listeners.Remove(resumeListener);
        //
        //     if (listener is IGameUpdateListener updateListener)
        //         _listeners.Remove(updateListener);
        //
        //     if (listener is IGameFixedUpdateListener fixedUpdateListener)
        //         _listeners.Remove(fixedUpdateListener);
        // }
        public void SetState(GameState state)
        {
            if (_state == state)
                return;

            switch (state)
            {
                case GameState.PLAYING:
                    ListenersManager.IsUpdatesEnabled = true;
                    if (_state == GameState.PAUSED)
                        ListenersManager.Invoke<IGameResumeListener>(listener => listener.OnResumeGame());
                    else
                        ListenersManager.Invoke<IGameStartListener>(listener => listener.OnStartGame());
                    //_listeners.IsEnable = true;
                    // if (_state == GameState.PAUSED)
                    //     _listeners.CacheForEach<IGameResumeListener>(listener => listener.OnResumeGame());
                    // else
                    //     _listeners.CacheForEach<IGameStartListener>(listener => listener.OnStartGame());
                    break;
                case GameState.PAUSED:
                    ListenersManager.Invoke<IGamePauseListener>(listener => listener.OnPauseGame());
                    ListenersManager.IsUpdatesEnabled = false;
                    // _listeners.CacheForEach<IGamePauseListener>(listener => listener.OnPauseGame());
                    // _listeners.IsEnable = false;
                    break;
                case GameState.FINISHED:
                    ListenersManager.Invoke<IGameFinishListener>(listener => listener.OnFinishGame());
                    ListenersManager.IsUpdatesEnabled = false;
                    // _listeners.CacheForEach<IGameFinishListener>(listener => listener.OnFinishGame());
                    // _listeners.IsEnable = false;
                    break;
            }

            _state = state;
        }
        private bool IsPlaying()
        {
            return State == GameState.PLAYING;
        }
        
        // private class Listeners<TValue>
        // {
        //     private readonly Dictionary<Type, HashSet<TValue>> _listeners = new();
        //
        //     public bool IsEnable { get; set; }
        //
        //     public void Add<T>(T value) where T : TValue
        //     {
        //         var type = typeof(T);
        //         if (!_listeners.ContainsKey(type))
        //             _listeners.Add(type, new HashSet<TValue>());
        //
        //         _listeners[type].Add(value);
        //     }
        //     public void Remove<T>(T value) where T : TValue
        //     {
        //         var type = typeof(T);
        //         if (!_listeners.ContainsKey(type))
        //             return;
        //
        //         _listeners[type].Remove(value);
        //     }
        //     public bool ContainsKey(Type key)
        //     {
        //         return _listeners.ContainsKey(key);
        //     }
        //     public bool ContainsValue<T>(T value)  where T : TValue
        //     {
        //         var type = typeof(T);
        //         if (!ContainsKey(type))
        //             return false;
        //         
        //         return _listeners[type].Contains(value);
        //     }
        //     public void ForEach<T>(Action<T> action) where T : TValue
        //     {
        //         if(action == null)
        //             return;
        //         
        //         var type = typeof(T);
        //         if(!_listeners.ContainsKey(type))
        //             return;
        //     
        //         foreach (var value in _listeners[type])
        //         {
        //             action.Invoke((T)value);
        //         }
        //     }
        //     public TValue[] Get<T>() where T : TValue
        //     {
        //         var type = typeof(T);
        //         return _listeners[type].ToArray();
        //     }
        //     public void CacheForEach<T>(Action<T> action) where T : TValue
        //     {
        //         if (action == null)
        //             return;
        //
        //         var type = typeof(T);
        //         if (!_listeners.ContainsKey(type))
        //             return;
        //
        //         foreach (var value in _listeners[type].ToArray())
        //         {
        //             if(!IsEnable)
        //                 break;
        //             
        //             action.Invoke((T)value);
        //         }
        //     }
        // }
    }
}