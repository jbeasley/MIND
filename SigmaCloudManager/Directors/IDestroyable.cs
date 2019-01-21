using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.Directors
{
    /// <summary>
    /// Generic interface implemented by directors capable of destroying entity objects.
    /// </summary>
	public interface IDestroyable<T>
    {
        Task DestroyAsync(T item, bool cleanUpNetwork = false);
        Task DestroyAsync(List<T> items, bool cleanUpNetwork = false);
    }
}
