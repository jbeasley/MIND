using SCM.Models.RequestModels;
using System.Threading.Tasks;

namespace SCM.Factories
{
    public interface IVifFactory
    {
        Task<FactoryResult> NewAsync(VifRequest request);
    }
}
