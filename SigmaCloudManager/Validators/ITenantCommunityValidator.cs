using System.Threading.Tasks;
using SCM.Models;

namespace SCM.Validators
{
    public interface ITenantCommunityValidator : IValidator
    {
        Task ValidateNewAsync(TenantCommunity tenantCommunity);
        Task ValidateDeleteAsync(int tenantCommunityId);
        Task ValidateChangesAsync(TenantCommunity tenantCommunity);
    }
}
