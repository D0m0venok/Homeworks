using UnityEngine;

namespace VG.Utilites
{
    public interface IEntityListener : IListener{}
    
    public interface IAwake : IEntityListener
    {
        void OnEntityAwake();
    }
    public interface IStart : IEntityListener
    {
        void OnEntityStart();
    }
    public interface IEnable : IEntityListener
    {
        void OnEntityEnable();
    }
    public interface IDisable : IEntityListener
    {
        void OnEntityDisable();
    }
    public interface IDestroy : IEntityListener
    {
        void OnEntityDestroy();
    }
    public interface IBecameVisible : IEntityListener
    {
        void OnEntityBecameVisible();
    }
    public interface IBecameInvisible : IEntityListener
    {
        void OnEntityBecameInvisible();
    }
    public interface ICollisionEnter : IEntityListener
    {
        void OnEntityCollisionEnter(Collision other);
    }
    public interface ICollisionStay : IEntityListener
    {
        void OnEntityCollisionStay(Collision other);
    }
    public interface ICollisionExit : IEntityListener
    {
        void OnEntityCollisionExit(Collision other);
    }
    public interface ICollisionEnter2D : IEntityListener
    {
        void OnEntityCollisionEnter2D(Collision2D other);
    }
    public interface ICollisionStay2D : IEntityListener
    {
        void OnEntityCollisionStay2D(Collision2D other);
    }
    public interface ICollisionExit2D : IEntityListener
    {
        void OnEntityCollisionExit2D(Collision2D other);
    }
}