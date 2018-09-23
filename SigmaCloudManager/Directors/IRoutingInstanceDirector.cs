﻿using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Builders
{
    public interface IRoutingInstanceDirector
    {
        Task<SCM.Models.RoutingInstance> BuildAsync(Device device);
    }
}
