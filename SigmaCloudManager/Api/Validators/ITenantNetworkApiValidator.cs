using SCM.Api.Models;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface ITenantNetworkApiValidator : IApiValidator
    {
        Task ValidateNewAsync(TenantNetworkRequestApiModel apiModel);
        Task ValidateDeleteAsync(TenantNetwork tenantNetwork);
    }
}