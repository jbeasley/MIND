using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using SCM.Validators;
using Mind.Services;

namespace SCM.Services
{
    public class VpnTenantIpNetworkStaticRouteRoutingInstanceService : BaseService, IVpnTenantIpNetworkStaticRouteRoutingInstanceService
    {
        private readonly string _properties = "AttachmentSet,"
        + "RoutingInstance,"
        + "TenantIpNetwork";
        private readonly IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator _validator;

        public VpnTenantIpNetworkStaticRouteRoutingInstanceService(IUnitOfWork unitOfWork, IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator validator) : 
            base(unitOfWork, validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Get all tenant IP network static routes for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkStaticRouteRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all vpn tenant IP network static routes for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkStaticRouteRoutingInstance>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id).Any(),
                         includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                         AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant IP network static route.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkStaticRouteRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.GetAsync(
                q =>
                   q.VpnTenantIpNetworkStaticRouteRoutingInstanceID == id,
                   includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                   AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkStaticRouteRoutingInstance> AddAsync(VpnTenantIpNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            await _validator.ValidateNewAsync(vpnTenantNetworkStaticRouteRoutingInstance);
            if (!_validator.IsValid) throw new ServiceValidationException();
            
            this.UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.Insert(vpnTenantNetworkStaticRouteRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.VpnTenantIpNetworkStaticRouteRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkStaticRouteRoutingInstance> UpdateAsync(VpnTenantIpNetworkStaticRouteRoutingInstance vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.Update(vpnTenantNetworkStaticRouteRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.VpnTenantIpNetworkStaticRouteRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkStaticRouteRoutingInstanceId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkStaticRouteRoutingInstanceRepository.DeleteAsync(vpnTenantNetworkStaticRouteRoutingInstanceId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}