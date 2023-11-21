using ShootEmUp;
using UnityEngine;

public abstract class RigidbodyStateController : MonoBehaviour, 
    IGameFinishListener, IGamePauseListener, IGameResumeListener
{
    [SerializeField] protected Rigidbody2D _rigidbody2D;

    private Vector2 _cacheVelocity;

    public void OnFinishGame()
    {
        Sleep();
    }
    public void OnPauseGame()
    {
        Sleep();
    }
    public void OnResumeGame()
    { 
        WakeUp();
    }
    
    private void Sleep()
    {
        _cacheVelocity = _rigidbody2D.velocity;
        _rigidbody2D.Sleep();
    }
    private void WakeUp()
    {
        _rigidbody2D.WakeUp();
        _rigidbody2D.velocity = _cacheVelocity;
    }
}