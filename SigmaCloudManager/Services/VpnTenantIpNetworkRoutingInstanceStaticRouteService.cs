using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using SCM.Validators;
using Mind.Services;
using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
using Mind.Builders;

namespace SCM.Services
{
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteService : BaseService, IVpnTenantIpNetworkRoutingInstanceStaticRouteService
    {
        private IRoutingInstanceStaticRouteDirector _director;
        private IRoutingInstanceStaticRouteUpdateDirector _updateDirector;

        public VpnTenantIpNetworkRoutingInstanceStaticRouteService(IUnitOfWork unitOfWork, 
            IRoutingInstanceStaticRouteDirector director,
            IRoutingInstanceStaticRouteUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all tenant IP network static routes for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, 
            bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(
                q => 
                    q.AttachmentSetID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                    AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all vpn tenant IP network static routes for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstanceStaticRoute>> GetAllByVpnIDAsync(int id, bool? deep = false, 
            bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id)
                         .Any(),
                          query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                          AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single tenant IP network static route.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.GetAsync(
                q =>
                   q.VpnTenantIpNetworkRoutingInstanceStaticRouteID == id,
                   query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                   AsTrackable: asTrackable)
                   select result)
                   .SingleOrDefault();
        }

        /// <summary>
        /// TO BE REMOVED
        /// </summary>
        /// <param name="vpnTenantNetworkRoutingInstanceStaticRoute"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> AddAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkRoutingInstanceStaticRoute)
        {   
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.Insert(vpnTenantNetworkRoutingInstanceStaticRoute);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkRoutingInstanceStaticRoute.VpnTenantIpNetworkRoutingInstanceStaticRouteID, 
                deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> AddAsync(int attachmentSetId, VpnTenantIpNetworkRoutingInstanceStaticRouteRequest request)
        {
            var vpnTenantIpNetworkRoutingInstanceStaticRoute = await _director.BuildAsync(attachmentSetId, request);
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.Insert(vpnTenantIpNetworkRoutingInstanceStaticRoute);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkRoutingInstanceStaticRoute.VpnTenantIpNetworkRoutingInstanceStaticRouteID, 
                deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnTenantNetworkStaticRouteRoutingInstance"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync(VpnTenantIpNetworkRoutingInstanceStaticRoute vpnTenantNetworkStaticRouteRoutingInstance)
        {
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.Update(vpnTenantNetworkStaticRouteRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRouteID, 
                deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkRoutingInstanceStaticRoute> UpdateAsync(int vpnTenantIpNetworkRoutingInstanceStaticRouteId, VpnTenantIpNetworkRoutingInstanceStaticRouteUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantIpNetworkRoutingInstanceStaticRouteId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceStaticRouteRepository.DeleteAsync(vpnTenantIpNetworkRoutingInstanceStaticRouteId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}