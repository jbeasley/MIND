using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public class DefaultRoutingInstanceDirector : IRoutingInstanceDirector
    {
        private readonly IDefaultRoutingInstanceBuilder _builder;

        public DefaultRoutingInstanceDirector(IDefaultRoutingInstanceBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.RoutingInstance> BuildAsync(Device device)
        {
            return await _builder.ForDevice(device)
                                 .BuildAsync();
        }
    }
}
