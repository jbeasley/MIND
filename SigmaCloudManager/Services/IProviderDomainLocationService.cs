using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IProviderDomainLocationService : IBaseService
    {
        Task<IEnumerable<Location>> GetAllAsync(bool? deep = false, bool asTrackable = false, string regionName = "", string subRegionName = "");
        Task<Location> GetByIDAsync(int locationId, bool? deep = false, bool asTrackable = false);
    }
}
