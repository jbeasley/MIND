using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IRoutingInstanceBuilder
    {
        Task<RoutingInstance> BuildAsync();
    }
}
