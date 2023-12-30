namespace VG.Utilites
{
    public abstract class EntityComponent : IEntityListener, IManagerListener
    {
        public virtual void Init(Entity entity)
        {
            if(Entity == null)
                Entity = entity;
        }

        public Entity Entity { get; private set; }
    }
}