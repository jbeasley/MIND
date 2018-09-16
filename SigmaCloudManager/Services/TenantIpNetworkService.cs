using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Validators;
using SCM.Services;
using System.Net;
using Mind.Builders;
using Mind.Models.RequestModels;

namespace Mind.Services
{
    public class TenantIpNetworkService : BaseService, ITenantIpNetworkService
    {
        private readonly string _properties = "Tenant," +
                    "VpnTenantIpNetworksIn.AttachmentSet," +
                    "VpnTenantIpNetworksOut.AttachmentSet," +
                    "VpnTenantIpNetworksRoutingInstance";

        private readonly ITenantIpNetworkValidator _validator;
        private readonly ITenantIpNetworkDirector _director;
        private readonly ITenantIpNetworkUpdateDirector _updateDirector;

        public TenantIpNetworkService(IUnitOfWork unitOfWork, ITenantIpNetworkDirector director, 
            ITenantIpNetworkUpdateDirector updateDirector, ITenantIpNetworkValidator validator) : base(unitOfWork, validator)
        {
            _validator = validator;
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<IEnumerable<TenantIpNetwork>> GetAllByTenantIDAsync(int id, string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await this.UnitOfWork.TenantIpNetworkRepository.GetAsync(q => q.TenantID == id,
                     includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                     AsTrackable: asTrackable)
                         select result);

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(q => q.CidrName == searchString);
            return query.ToList();
        }

        public async Task<TenantIpNetwork> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantIpNetworkRepository.GetAsync(q => q.TenantIpNetworkID == id,
                   includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                   AsTrackable: asTrackable)
                   select result)
                   .SingleOrDefault();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="tenantIpNetwork"></param>
        /// <returns></returns>
        public async Task<TenantIpNetwork> AddAsync(TenantIpNetwork tenantIpNetwork)
        {
            this.UnitOfWork.TenantIpNetworkRepository.Insert(tenantIpNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetwork.TenantIpNetworkID, deep: true);
        }

        public async Task<TenantIpNetwork> AddAsync(int tenantId, TenantIpNetworkRequest request)
        {
            var tenantIpNetwork = await _director.BuildAsync(tenantId, request);
            this.UnitOfWork.TenantIpNetworkRepository.Insert(tenantIpNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetwork.TenantIpNetworkID, deep: true);
        }

        public async Task DeleteAsync(int tenantNetworkId)
        {
            await _validator.ValidateDeleteAsync(tenantNetworkId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            await this.UnitOfWork.TenantIpNetworkRepository.DeleteAsync(tenantNetworkId);
            await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="tenantIpNetwork"></param>
        /// <returns></returns>
        public async Task<TenantIpNetwork> UpdateAsync(TenantIpNetwork tenantIpNetwork)
        {
            this.UnitOfWork.TenantIpNetworkRepository.Update(tenantIpNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetwork.TenantIpNetworkID, deep: true);
        }

        public async Task<TenantIpNetwork> UpdateAsync(int tenantIpNetworkId, TenantIpNetworkRequest update)
        {
            await _updateDirector.UpdateAsync(tenantIpNetworkId, update);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetworkId, deep: true);
        }
    }
}
