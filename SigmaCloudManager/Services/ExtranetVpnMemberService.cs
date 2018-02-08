using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class ExtranetVpnMemberService : BaseService, IExtranetVpnMemberService
    {
        private string Properties { get; } = "ExtranetVpn.Tenant,"
               + "MemberVpn.Tenant";

        public ExtranetVpnMemberService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<ExtranetVpnMember>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.ExtranetVpnMemberRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<ExtranetVpnMember> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.ExtranetVpnMemberRepository.GetAsync(q => q.ExtranetVpnMemberID == id, 
                includeProperties: p, AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<ExtranetVpnMember>> GetAllByExtranetVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.ExtranetVpnMemberRepository.GetAsync(q => q.ExtranetVpnID == id,
                includeProperties: p, AsTrackable: false);
        }

        public async Task<IEnumerable<ExtranetVpnMember>> GetAllByMemberVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.ExtranetVpnMemberRepository.GetAsync(q => q.MemberVpnID == id,
                includeProperties: p, AsTrackable: false);
        }

        public async Task<int> AddAsync(ExtranetVpnMember extranetVpnMember)
        {
            this.UnitOfWork.ExtranetVpnMemberRepository.Insert(extranetVpnMember);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(ExtranetVpnMember extranetVpnMember)
        {
            this.UnitOfWork.ExtranetVpnMemberRepository.Update(extranetVpnMember);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(ExtranetVpnMember extranetVpnMember)
        {
            this.UnitOfWork.ExtranetVpnMemberRepository.Delete(extranetVpnMember);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
