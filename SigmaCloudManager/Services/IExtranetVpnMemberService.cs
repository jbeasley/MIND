using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IExtranetVpnMemberService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<ExtranetVpnMember>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnMember>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<ExtranetVpnMember>> GetAllByMemberVpnIDAsync(int id, bool includeProperties = true);
        Task<ExtranetVpnMember> GetByIDAsync(int id, bool includeProperties = true);
        Task<int> AddAsync(ExtranetVpnMember extranetVpnMember);
        Task<int> UpdateAsync(ExtranetVpnMember extranetVpnMember);
        Task<int> DeleteAsync(ExtranetVpnMember extranetVpnMember);
    }
}
