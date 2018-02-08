using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Factories
{
    public interface IDeviceFactory
    {
        Task<FactoryResult> NewAsync(Device device);
    }
}
