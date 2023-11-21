namespace ShootEmUp
{
    public interface IGameListener{}    
    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }
    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }
    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }
    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }
    public interface IGameAttachListener : IGameListener
    {
        void AttachGame();
    }
    public interface IGameDetachListener : IGameListener
    {
        void DetachGame();
    }
}