using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.Directors
{
    /// <summary>
    /// Generic interface implemented by directors capable of performing network synchronization.
    /// </summary>
    public interface INetworkSynchronizable<T>
    {
        Task<T> SyncToNetworkPutAsync(T item);
    }
}
