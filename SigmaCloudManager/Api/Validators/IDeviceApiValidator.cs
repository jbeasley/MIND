using SCM.Api.Models;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Api.Validators
{
    public interface IDeviceApiValidator : IApiValidator
    {
        Task ValidateNewAsync(DeviceRequestApiModel request);
        void ValidateDelete(Device device);
    }
}