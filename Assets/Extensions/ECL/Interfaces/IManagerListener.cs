namespace VG.Utilites
{
    public interface IManagerListener : IListener{}
    
    public interface IUpdate : IManagerListener
    {
        void OnEntityUpdate();
    }
    public interface ILateUpdate : IManagerListener
    {
        void OnEntityLateUpdate();
    }
    public interface IFixedUpdate : IManagerListener
    {
        void OnEntityFixedUpdate();
    }
}