using System.Collections.Generic;

namespace ShootEmUp
{
    public interface IGameListenerProvider
    {
        IEnumerable<IGameListener> ProvideListeners();
    }
}