namespace VG.Utilites
{
    public abstract class SelfInjected
    {
        protected SelfInjected()
        {
            DIContainer.InjectTo(GetType(), this);
        }
    }
}