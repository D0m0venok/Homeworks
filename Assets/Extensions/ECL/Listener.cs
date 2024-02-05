namespace VG.Utilites
{
    public abstract class Listener : IManagerListener
    {
        protected Listener()
        {
            ListenersManager.Add(this);
        }
        
        ~Listener()
        {
            ListenersManager.Remove(this);
        }
    }
}