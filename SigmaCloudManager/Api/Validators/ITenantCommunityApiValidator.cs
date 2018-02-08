using SCM.Api.Models;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface ITenantCommunityApiValidator : IApiValidator
    {
        Task ValidateNewAsync(TenantCommunityRequestApiModel apiModel);
        Task ValidateDeleteAsync(TenantCommunity tenantNetwork);
    }
}