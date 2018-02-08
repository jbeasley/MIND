using SCM.Api.Models;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Validators
{
    public interface IRouteTargetValidator : IValidator
    {
        Task ValidateNewAsync(RouteTargetRequest request);
        void ValidateExisting(Vpn vpn);
        Task ValidateRouteTargetsChangeableAsync(Vpn vpn);
    }
}
