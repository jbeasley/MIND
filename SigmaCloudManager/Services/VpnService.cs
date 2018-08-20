using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Factories;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public class VpnService : BaseService, IVpnService
    {
        public VpnService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IVpnFactory vpnFactory) : base(unitOfWork, mapper)
        {
            VpnFactory = vpnFactory;
        }

        private IVpnFactory VpnFactory { get; }
        private string Properties { get; } = "Region,"
                + "Plane,"
                + "VpnTenancyType,"
                + "MulticastVpnServiceType,"
                + "MulticastVpnDirectionType,"
                + "VpnTopologyType.VpnProtocolType,"
                + "Tenant,"
                + "ExtranetVpnMembers.MemberVpn,"
                + "ExtranetVpns.ExtranetVpn,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Device.Location.SubRegion.Region,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Attachments.Tenant,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Attachments.Interfaces.Ports,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Vifs.Attachment.Interfaces.Ports,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Vifs.Tenant,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Tenant,"
                + "VpnAttachmentSets.AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.BgpPeers,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworksIn.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesIn.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworkStaticRoutesRoutingInstance.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworksIn.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworksIn.VpnTenantNetworkCommunitiesIn.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesIn.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworksOut.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesOut.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworksRoutingInstance.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesRoutingInstance.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesRoutingInstance.TenantCommunitySet.RoutingPolicyMatchOption,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantCommunitiesRoutingInstance.TenantCommunitySet.TenantCommunitySetCommunities.TenantCommunity,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantNetworkStaticRoutesRoutingInstance.TenantNetwork,"
                + "VpnAttachmentSets.AttachmentSet.Tenant,"
                + "VpnAttachmentSets.AttachmentSet.MulticastVpnRps.VpnTenantMulticastGroups.TenantMulticastGroup,"
                + "VpnAttachmentSets.AttachmentSet.VpnTenantMulticastGroups.TenantMulticastGroup,"
                + "VpnAttachmentSets.AttachmentSet.MulticastVpnDomainType,"
                + "RouteTargets.RouteTargetRange,"
                + "AddressFamily";

        /// <summary>
        /// Handler for ordering of VPN records. Records are sorted according to 
        /// a key value such as 'Name'
        /// </summary>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        private Func<IQueryable<Vpn>, IOrderedQueryable<Vpn>> OrderBy(string sortKey)
        {
            switch (sortKey)
            {
                case "Name_Desc":
                    return x => x.OrderByDescending(item => item.Name);

                case "TenancyType":
                    return x => x.OrderBy(item => item.VpnTenancyType.TenancyType);

                case "TenancyType_Desc":
                    return x => x.OrderByDescending(item => item.VpnTenancyType.TenancyType);

                case "Tenant":
                    return x => x.OrderBy(item => item.Tenant);

                case "Tenant_Desc":
                    return x => x.OrderByDescending(item => item.Tenant);

                case "Plane":
                    return x => x.OrderBy(item => item.Plane.Name);

                case "Plane_Desc":
                    return x => x.OrderByDescending(item => item.Plane.Name);

                case "Region":
                    return x => x.OrderBy(item => item.Region.Name);

                case "Region_Desc":
                    return x => x.OrderByDescending(item => item.Region.Name);

                default:
                    return x => x.OrderBy(item => item.Name);
            }
        }

        /// <summary>
        /// Return all VPNs.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllAsync(bool? isExtranet = null, 
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, 
            string searchString = "", bool includeProperties = true, string sortKey = "")
        {
            var p = includeProperties ? Properties : string.Empty;
            var orderBy = OrderBy(sortKey);

            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(includeProperties: p,
                    AsTrackable: false,
                    orderBy: orderBy)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }

            if (isExtranet != null)
            {
                query = query.Where(x => x.IsExtranet = isExtranet.Value);
            }

            if (requiresSync != null)
            {
                query = query.Where(x => x.RequiresSync);
            }

            if (created != null)
            {
                query = query.Where(x => x.Created);
            }

            if (showRequiresSyncAlert != null)
            {
                query = query.Where(x => x.ShowRequiresSyncAlert);
            }

            if (showCreatedAlert != null)
            {
                query = query.Where(x => x.ShowCreatedAlert);
            }

            return query.ToList();
        }

        /// <summary>
        /// Return a single VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<Vpn> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnID == id, 
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Return all VPNs which are associated with a given VRF.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByRoutingInstanceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                    .SelectMany(r => r.AttachmentSet.AttachmentSetRoutingInstances)
                    .Where(s => s.RoutingInstanceID == id)
                    .Any(),
                includeProperties: p,
                AsTrackable: false);

            return dbResult.GroupBy(q => q.VpnID).Select(r => r.First());
        }

        /// <summary>
        /// Return all VPNs which are associated with a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByAttachmentSetIDAsync(int id, bool? requiresSync = null, bool? created = null, 
            bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null,bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                    .Where(r => r.AttachmentSetID == id)
                    .Any(),
                includeProperties: p,
                AsTrackable: false)
                        select vpns;

            if (requiresSync != null)
            {
                query = query.Where(x => x.RequiresSync);
            }

            if (created != null)
            {
                query = query.Where(x => x.Created);
            }

            if (showRequiresSyncAlert != null)
            {
                query = query.Where(x => x.ShowRequiresSyncAlert);
            }

            if (showCreatedAlert != null)
            {
                query = query.Where(x => x.ShowCreatedAlert);
            }

            return query.ToList();
        }

        /// <summary>
        /// Return all VPNs for a given Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantIpNetworkIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var tasks = new List<Task<IList<Vpn>>> {
                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksIn)
                                                        .Select(y => y.TenantIpNetwork)
                                                        .Where(x => x.TenantIpNetworkID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksOut)
                                                        .Select(y => y.TenantIpNetwork)
                                                        .Where(x => x.TenantIpNetworkID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksRoutingInstance)
                                                        .Select(y => y.TenantIpNetwork)
                                                        .Where(x => x.TenantIpNetworkID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworkStaticRoutesRoutingInstance)
                                                        .Select(y => y.TenantIpNetwork)
                                                        .Where(x => x.TenantIpNetworkID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false)
            };
            var results = new List<Vpn>();

            while (tasks.Count() > 0)
            {
                Task<IList<Vpn>> task = await Task.WhenAny(tasks);
                results.AddRange(task.Result);
                tasks.Remove(task);
            }

            await Task.WhenAll(tasks);

            return results.GroupBy(q => q.VpnID).Select(r => r.First());
        }

        /// <summary>
        /// Return all VPNs for a given Tenant Community.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantCommunityIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            
            var tasks = new List<Task<IList<Vpn>>> {
                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesIn)
                                                        .Select(y => y.TenantCommunity)
                                                        .Where(x => x.TenantCommunityID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesOut)
                                                        .Select(y => y.TenantCommunity)
                                                        .Where(x => x.TenantCommunityID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .Where(x => x.TenantCommunity != null)
                                                        .Select(x => x.TenantCommunity)
                                                        .Where(x => x.TenantCommunityID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false),

                this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantCommunitiesRoutingInstance)
                                                        .SelectMany(x => x.TenantCommunitySet.TenantCommunitySetCommunities)
                                                        .Where(x => x.TenantCommunitySet != null)
                                                        .Select(x => x.TenantCommunity)
                                                        .Where(x => x.TenantCommunityID == id)
                                                        .Any(), includeProperties: p, AsTrackable: false)
            };

            var results = new List<Vpn>();

            while (tasks.Count() > 0)
            {
                Task<IList<Vpn>> task = await Task.WhenAny(tasks);
                results.AddRange(task.Result);
                tasks.Remove(task);
            }

            await Task.WhenAll(tasks);

            return results.GroupBy(q => q.VpnID).Select(r => r.First());
        }

        /// <summary>
        /// Return all VPNs for a given Tenant Community Set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantCommunitySetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                    .Select(x => x.AttachmentSet)
                    .SelectMany(r => r.VpnTenantCommunitiesRoutingInstance)
                    .Where(s => s.TenantCommunitySetID == id)
                    .Any(),
                includeProperties: p,
                AsTrackable: false);

            return dbResult.GroupBy(q => q.VpnID).Select(r => r.First());
        }

        /// <summary>
        /// Return all VPNs for a given Tenant Multicast Group.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantMulticastGroupIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                    .Select(x => x.AttachmentSet)
                    .SelectMany(x => x.MulticastVpnRps)
                    .SelectMany(x => x.VpnTenantMulticastGroups)
                    .Where(x => x.TenantMulticastGroupID == id)
                    .Any(),
                includeProperties: p,
                AsTrackable: false);

            return dbResult.GroupBy(q => q.VpnID).Select(r => r.First());
        }


        /// <summary>
        /// Return all VPNs for a given Tenant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantIDAsync(int id, bool? isExtranet = null, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(q => q.TenantID == id,
                includeProperties: p,
                AsTrackable: false)
                select vpns;

            if (isExtranet.HasValue)
            {
                query = query.Where(x => x.IsExtranet == isExtranet.Value);
            }

            return query.ToList().GroupBy(q => q.VpnID).Select(r => r.First());
        }

        public async Task<ServiceResult> AddAsync(VpnRequest vpnRequest)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            var vpnFactoryResult = await VpnFactory.NewAsync(vpnRequest);
            if (!vpnFactoryResult.IsSuccess)
            {
                result.IsSuccess = false;
                result.AddRange(vpnFactoryResult.Messages);

                return result;
            }

            var vpn = (Vpn)vpnFactoryResult.Item;
            result.Item = vpn;
            this.UnitOfWork.VpnRepository.Insert(vpn);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Update a Vpn.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAsync(Vpn vpn)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = vpn
            };

            this.UnitOfWork.VpnRepository.Update(vpn);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Update a collection of Vpns.
        /// </summary>
        /// <param name="vpns"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<Vpn> vpns)
        {
            foreach (var vpn in vpns)
            {
                this.UnitOfWork.VpnRepository.Update(vpn);
            }

            return await this.UnitOfWork.SaveAsync();
        }


        /// <summary>
        /// Delete a Vpn.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteAsync(Vpn vpn)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            this.UnitOfWork.VpnRepository.Delete(vpn);
            await this.UnitOfWork.SaveAsync();

            return result;
        }
       
        /// <summary>
        /// Helper to execute a collection of async tasks for a VPN
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        private async Task<IEnumerable<ServiceResult>> VpnTasksAsync(IList<Task<ServiceResult>> tasks,
            IProgress<ServiceResult> progress)
        {
            var results = new List<ServiceResult>();

            while (tasks.Count() > 0)
            {
                Task<ServiceResult> task = await Task.WhenAny(tasks);
                results.Add(task.Result);
                tasks.Remove(task);

                // Update caller with progress

                progress.Report(task.Result);
            }

            await Task.WhenAll(tasks);

            foreach (var result in results)
            {
                var vpn = (Vpn)result.Item;
                UnitOfWork.VpnRepository.Update(vpn);
                await UnitOfWork.SaveAsync();
            }

            return results;
        }
    }
}