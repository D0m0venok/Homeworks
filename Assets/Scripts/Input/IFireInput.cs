using System;

namespace ShootEmUp
{
    public interface IFireInput
    {
        public event Action OnFired;
    }
}