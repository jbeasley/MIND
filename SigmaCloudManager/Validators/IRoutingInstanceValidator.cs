using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Api.Models;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Validators
{
    public interface IRoutingInstanceValidator : IValidator
    {
        Task ValidateChangesAsync(RoutingInstanceUpdate routingInstanceUpdate);
    }
}
