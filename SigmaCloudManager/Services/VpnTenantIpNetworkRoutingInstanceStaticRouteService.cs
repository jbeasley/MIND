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
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteService : BaseService, IVpnTenantIpNetworkRoutingInstanceStaticRouteService
    {
        private readonly string _properties = "AttachmentSet,"
        + "RoutingInstance,"
        + "TenantIpNetwork";
        private readonly IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator _validator;

        public VpnTenantIpNetworkRoutingInstanceStaticRouteService(IUnitOfWork unitOfWork, IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator validator) : 
            base(unitOfWork, validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Get all tenant IP network static routes for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all vpn tenant IP network static routes for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id).Any(),
                         includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                         AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant IP network static route.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(
                q =>
                   q.VpnTenantIpNetworkRoutingInstanceStaticRouteID == id,
                   includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                   AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> AddAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkStaticRouteRoutingInstance)
        {
            await _validator.ValidateNewAsync(vpnTenantNetworkStaticRouteRoutingInstance);
            if (!_validator.IsValid) throw new ServiceValidationException();
            
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.Insert(vpnTenantNetworkStaticRouteRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRouteID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.Update(vpnTenantNetworkStaticRouteRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRouteID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkStaticRouteRoutingInstanceId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.DeleteAsync(vpnTenantNetworkStaticRouteRoutingInstanceId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}