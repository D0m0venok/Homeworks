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

    [InstallMono]
    public sealed class GameManager : MonoBehaviour
    {
        private GameState _state;

        public GameState State => _state;

        private void Awake()
        {
            ListenersManager.IsUpdatesEnabled = false;
        }

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
                    ListenersManager.IsUpdatesEnabled = false;
                    ListenersManager.Invoke<IGamePauseListener>(listener => listener.OnPauseGame());
                    // _listeners.CacheForEach<IGamePauseListener>(listener => listener.OnPauseGame());
                    // _listeners.IsEnable = false;
                    break;
                case GameState.FINISHED:
                    ListenersManager.IsUpdatesEnabled = false;
                    ListenersManager.Invoke<IGameFinishListener>(listener => listener.OnFinishGame());
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