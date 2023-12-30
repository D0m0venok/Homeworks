namespace VG.Utilites
{
    public abstract class SelfInjected
    {
        protected SelfInjected()
        {
            DI.Container.InjectTo(GetType(), this);
        }
    }
}