using SCM.Api.Models;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IPortApiValidator : IApiValidator
    {
        Task ValidateNewAsync(PortRequestApiModel request);
        Task ValidateDeleteAsync(Port port);
    }
}