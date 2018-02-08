using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using SCM.Models.RequestModels;
using AutoMapper;
using SCM.Factories;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class RouteTargetService : BaseService, IRouteTargetService
    {
        public RouteTargetService(IRouteTargetFactory routeTargetFactory, 
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
            RouteTargetFactory = routeTargetFactory;
        }

        private IRouteTargetFactory RouteTargetFactory { get; set; }
        private string Properties { get; } = "Vpn.Plane,"
                + "Vpn.VpnTenancyType,"
                + "Vpn.VpnTopologyType,"
                + "Vpn.Tenant,"
                + "Vpn.Region,"
                + "RouteTargetRange";

        public async Task<IEnumerable<RouteTarget>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.RouteTargetRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        public async Task<RouteTarget> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.RouteTargetRepository.GetAsync(q => q.RouteTargetID == id, 
                                                                           includeProperties: p, 
                                                                           AsTrackable: false);
            return dbResult.SingleOrDefault();
        }
   
        public async Task<IEnumerable<RouteTarget>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.RouteTargetRepository.GetAsync(q => q.VpnID == id, 
                                                                   includeProperties: p,
                                                                   AsTrackable: false);
        }

        public async Task<ServiceResult> AddAsync(RouteTargetRequest request)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            var routeTargetFactoryResult = await RouteTargetFactory.NewAsync(request);
            var routeTarget = (RouteTarget)routeTargetFactoryResult.Item;
            this.UnitOfWork.RouteTargetRepository.Insert(routeTarget);

            // RTs have changed - must flag VPN as needing sync with network

            await UpdateVpnSyncStateAsync(routeTarget.VpnID);
            await this.UnitOfWork.SaveAsync();
        
            return result;
        }

        public async Task<int> DeleteAsync(RouteTarget routeTarget)
        {
            this.UnitOfWork.RouteTargetRepository.Delete(routeTarget);

            // RTs have changed - must flag VPN as needing sync with network

            await UpdateVpnSyncStateAsync(routeTarget.VpnID);
            return await this.UnitOfWork.SaveAsync();
        }
       
        /// <summary>
        /// Update the RequiresSync state of a Vpn.
        /// </summary>
        /// <param name="vpnID"></param>
        /// <param name="requiresSync"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int vpnID)
        {
            var vpn = await UnitOfWork.VpnRepository.GetByIDAsync(vpnID);
            vpn.RequiresSync = true;
            UnitOfWork.VpnRepository.Update(vpn);
        }
    }
}