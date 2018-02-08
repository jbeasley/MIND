using SCM.Api.Models;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface ITenantApiValidator : IApiValidator
    {
        Task ValidateDeleteAsync(Tenant tenant);
    }
}