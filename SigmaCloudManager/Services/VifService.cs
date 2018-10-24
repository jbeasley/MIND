using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Factories;

namespace SCM.Services
{
    /// <summary>
    /// Service logic for VIFs
    /// </summary>
    public class VifService : BaseService, IVifService
    {
        public VifService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IVifFactory vifFactory,
            IRoutingInstanceFactory vrfFactory,
            IContractBandwidthPoolFactory contractBandwidthPoolFactory,
            IAttachmentService attachmentService,
            IContractBandwidthPoolService contractBandwidthPoolService) : base(unitOfWork, mapper)
        {
            VifFactory = vifFactory;
            RoutingInstanceFactory = vrfFactory;
            ContractBandwidthPoolFactory = contractBandwidthPoolFactory;
            AttachmentService = attachmentService;
            ContractBandwidthPoolService = contractBandwidthPoolService;
        }

        private IVifFactory VifFactory { get; }
        private IRoutingInstanceFactory RoutingInstanceFactory { get; }
        private IContractBandwidthPoolFactory ContractBandwidthPoolFactory { get; }
        private IAttachmentService AttachmentService { get; }
        private IContractBandwidthPoolService ContractBandwidthPoolService { get; }

        private string Properties { get; } = "VifRole,"
                + "Attachment.Tenant,"
                + "VifRole.RoutingInstanceType,"
                + "Attachment.AttachmentRole,"
                + "Attachment.Device.Location.SubRegion.Region,"
                + "Attachment.Device.Plane,"
                + "Attachment.Device.DeviceRole,"
                + "Attachment.RoutingInstance.BgpPeers,"
                + "Attachment.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet,"
                + "Attachment.AttachmentBandwidth,"
                + "Attachment.ContractBandwidthPool.ContractBandwidth,"
                + "Attachment.Interfaces.Device,"
                + "Attachment.Interfaces.Ports.Device,"
                + "Attachment.Interfaces.Ports.PortBandwidth,"
                + "Attachment.Interfaces.Ports.Interface.Vlans.Vif,"
                + "Attachment.Vifs.Tenant,"
                + "Attachment.Vifs.RoutingInstance.BgpPeers,"
                + "Attachment.Vifs.Vlans.Vif.ContractBandwidthPool,"
                + "Attachment.Vifs.ContractBandwidthPool.Tenant,"
                + "Attachment.Vifs.ContractBandwidthPool.ContractBandwidth,"
                + "RoutingInstance.RoutingInstanceType,"
                + "RoutingInstance.BgpPeers,"
                + "RoutingInstance.Device,"
                + "RoutingInstance.Tenant,"
                + "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "Vlans,"
                + "ContractBandwidthPool.ContractBandwidth,"
                + "ContractBandwidthPool.Tenant,"
                + "Tenant";

        /// <summary>
        /// Get a Vif by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Vif> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VifRepository.GetAsync(q => q.VifID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all Vifs for a given Attachment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Vif>> GetAllByAttachmentIDAsync(int id, bool? roleRequireSyncToNetwork = null, bool? requiresSync = null,
            bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "VifRole";
            var query = from vifs in await UnitOfWork.VifRepository.GetAsync(q => q.AttachmentID == id,
                includeProperties: p,
                AsTrackable: false)
                        select vifs;

            if (roleRequireSyncToNetwork != null)
            {
                query = query.Where(x => x.VifRole.RequireSyncToNetwork);
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

            if (created != null)
            {
                query = query.Where(x => x.Created);
            }

            return query.ToList();
        }

        /// <summary>
        /// Return all VIFs for a given VRF.
        /// </summary>
        /// <param name="vrfID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vif>> GetAllByRoutingInstanceIDAsync(int vrfID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;

            return await UnitOfWork.VifRepository.GetAsync(q => q.RoutingInstanceID == vrfID, 
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Return all Vifs for a given VPN.
        /// </summary>
        /// <param name="vpnID"></param>
        /// <returns></returns>
        public async Task<List<Vif>> GetAllByVpnIDAsync(int vpnID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var result = await UnitOfWork.VifRepository
                .GetAsync(q => q.RoutingInstance.AttachmentSetRoutingInstances
                .SelectMany(r => r.AttachmentSet.VpnAttachmentSets)
                .Where(s => s.VpnID == vpnID)
                .Any(),
                includeProperties: p, 
                AsTrackable: false);

            return result.ToList();
        }

        /// <summary>
        /// Add a new Vif
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResult> AddAsync(VifRequest request)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            // Call Vif factory to create a new Vif

            var vifFactoryResult = await VifFactory.NewAsync(request);
            var vif = (Vif)vifFactoryResult.Item;
            result.Item = vif;
            UnitOfWork.VifRepository.Insert(vif);
            await UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Update a VIF
        /// </summary>
        /// <param name="vifUpdate"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAsync(VifUpdate update)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                // Get the existing VIF for update

                var dbResult = await this.UnitOfWork.VifRepository.GetAsync(q => q.VifID == update.VifID,
                includeProperties: "VifRole,Attachment.AttachmentRole,Attachment.Device.DeviceRole,Vlans,RoutingInstance.RoutingInstanceType");
                var vif = dbResult.Single();

                if (update.CreateNewRoutingInstance)
                {
                    // Request to create a new routing instance - check to delete old routing instance

                    var routingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(x => 
                        x.RoutingInstanceID == vif.RoutingInstanceID,
                        includeProperties: "Attachments,Vifs");
                    var routingInstance = routingInstanceResult.Single();

                    if (!routingInstance.Attachments.Any() && routingInstance.Vifs.Count == 1)
                    {
                        // The only Logical Attachment which belongs to the routing instance is the current VIF
                        // so the routing instance can be deleted

                        UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                    }

                    // Create a new routing instance

                    update.DeviceID = routingInstance.DeviceID;
                    update.TenantID = routingInstance.TenantID;
                    update.RoutingInstanceTypeID = routingInstance.RoutingInstanceTypeID;
                    var vrfFactoryResult = await RoutingInstanceFactory.NewAsync(update);
                    var newRoutingInstance = (RoutingInstance)vrfFactoryResult.Item;
                    UnitOfWork.RoutingInstanceRepository.Insert(newRoutingInstance);
                    await UnitOfWork.SaveAsync();

                    // Add new routing instance to the VIF

                    vif.RoutingInstanceID = newRoutingInstance.RoutingInstanceID;
                }

                else if (update.RoutingInstanceID != null && update.RoutingInstanceID != vif.RoutingInstanceID)
                {
                    // An existing routing instance has been selected - check to delete old routing instance

                    var routingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(x => 
                        x.RoutingInstanceID == vif.RoutingInstanceID,
                        includeProperties: "Attachments,Vifs");
                    var routingInstance = routingInstanceResult.Single();

                    if (!routingInstance.Attachments.Any() && routingInstance.Vifs.Count() == 1)
                    {
                        // The only Logical Attachment which belongs to the routing instance 
                        // is the current VIF so routing instance can be deleted

                        UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                    }

                    // Add new selected routing instance to the VIF

                    vif.RoutingInstanceID = update.RoutingInstanceID;
                    vif.RoutingInstance = null;
                }

                // Check if a new Contract Bandwidth Pool is requested

                if (update.ContractBandwidthID != null)
                {
                    // New contract bandwidth has been selected
                    // Check to delete the old Contract Bandwidth Pool

                    if (await CheckDeleteContractBandwidthPoolAsync(vif))
                    {
                        await this.UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(vif.ContractBandwidthPoolID);
                    }

                    // Create a new Contract Bandwidth Pool and add it to the VIF

                    var contractBandwidthPool = (ContractBandwidthPool)ContractBandwidthPoolFactory.New(Mapper.Map<ContractBandwidthPool>(update)).Item;

                    UnitOfWork.ContractBandwidthPoolRepository.Insert(contractBandwidthPool);
                    vif.ContractBandwidthPool = contractBandwidthPool;
                }

                if (update.ContractBandwidthPoolID != null)
                {
                    // The update request is to either retain the existing Contract Bandwidth Pool allocated to the VIF
                    // or to share am existing contract bandwidth pool with another VIF

                    // Get the contract bandwidth pool requested in the update and perform any updates requested
                    // on the contract bandwidth pool

                    var contractBandwidthPool = await ContractBandwidthPoolService.GetByIDAsync(update.ContractBandwidthPoolID.Value);
                    if (contractBandwidthPool.TrustReceivedCosAndDscp != update.TrustReceivedCosDscp)
                    {
                        contractBandwidthPool.TrustReceivedCosAndDscp = update.TrustReceivedCosDscp;
                        this.UnitOfWork.ContractBandwidthPoolRepository.Update(contractBandwidthPool);
                    }

                    if (vif.ContractBandwidthPoolID != update.ContractBandwidthPoolID)
                    {
                        // The update request is for the VIF to share a contract bandwidth pool with another VIF

                        // Update the VIFs which share the same Contract Bandwidth Pool to indicate 
                        // they require resync with the network

                        var vifs = await UnitOfWork.VifRepository.GetAsync(q => q.ContractBandwidthPoolID == update.ContractBandwidthPoolID,
                                includeProperties: "VifRole");

                        foreach (var v in vifs)
                        {
                            v.RequiresSync = v.VifRole.RequireSyncToNetwork;
                            v.ShowRequiresSyncAlert = v.VifRole.RequireSyncToNetwork;

                            this.UnitOfWork.VifRepository.Update(v);
                        }

                        // Check to delete the old Contract Bandwidth Pool

                        if (await CheckDeleteContractBandwidthPoolAsync(vif))
                        {
                            await this.UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(vif.ContractBandwidthPoolID);
                        }

                        // Update the VIF to use shared Contract Bandwidth Pool

                        vif.ContractBandwidthPool = null;
                        vif.ContractBandwidthPoolID = contractBandwidthPool.ContractBandwidthPoolID;
                    }
                }

                // Set 'RequiresSync' property of VIF, the parent Attachment, and the associated Device, 
                // to indicate these entities require sync with the network

                vif.RequiresSync = vif.VifRole.RequireSyncToNetwork;
                vif.ShowRequiresSyncAlert = vif.VifRole.RequireSyncToNetwork;
                vif.Attachment.RequiresSync = vif.Attachment.AttachmentRole.RequireSyncToNetwork;
                vif.Attachment.ShowRequiresSyncAlert = vif.Attachment.AttachmentRole.RequireSyncToNetwork;
                vif.Attachment.Device.RequiresSync = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;
                vif.Attachment.Device.ShowRequiresSyncAlert = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;

                this.UnitOfWork.VifRepository.Update(vif);
                this.UnitOfWork.AttachmentRepository.Update(vif.Attachment);
                this.UnitOfWork.DeviceRepository.Update(vif.Attachment.Device);
                await this.UnitOfWork.SaveAsync();
                result.Item = vif;

                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Update a collection of VIFs
        /// </summary>
        /// <param name="vifs"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<Vif> vifs)
        {
            foreach (var vif in vifs)
            {
                UnitOfWork.VifRepository.Update(vif);
            }

            return await UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Delete a vif from the network and from the inventory
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteAsync(Vif vif)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                // Delete vlans for the Vif
            
                var vlans = vif.Vlans;
                foreach (var vlan in vlans)
                {
                    UnitOfWork.VlanRepository.Delete(vlan);
                }

                // Check if the Contract Bandwidth Pool can be deleted (it can be deleted if this is
                // the last VIF which shares the Contract Bandwidth Pool

                if (await CheckDeleteContractBandwidthPoolAsync(vif))
                {
                    // Delete Contract Bandwidth Pool

                    await UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(vif.ContractBandwidthPoolID);
                }

                UnitOfWork.VifRepository.Delete(vif);
                await UnitOfWork.SaveAsync();

                // Check to delete the routing instance. The routing instance can be deleted 
                // if there are no more VIFs or Attachments which belong to it and it is not 
                // the default routing instance for the device

                var routingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.RoutingInstanceID == vif.RoutingInstanceID,
                    includeProperties: "Attachments,Vifs,RoutingInstanceType,BgpPeers,LogicalInterfaces");
                var routingInstance = routingInstanceResult.Single();

                if (!routingInstance.Vifs.Any() && !routingInstance.Attachments.Any())
                {
                    if (routingInstance.RoutingInstanceType.IsDefault)
                    {
                        // There are no more logical attachments whcih belong to the default routing instance
                        // Clean up BGP peers and Logical Interfaces which belong to the default routing instance

                        foreach (var bgpPeer in routingInstance.BgpPeers)
                        {
                            UnitOfWork.BgpPeerRepository.Delete(bgpPeer);
                        }
                        foreach (var logicalInterface in routingInstance.LogicalInterfaces)
                        {
                            UnitOfWork.LogicalInterfaceRepository.Delete(logicalInterface);
                        }
                    }
                    else
                    {
                        // Not the default routing instance, so delete the routing instance

                        UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                    }
                }

                await UnitOfWork.SaveAsync();
                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Helper to update a Vif
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private async Task<int> UpdateHelperAsync(Vif vif)
        {
            this.UnitOfWork.VifRepository.Update(vif);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Helper to check if a Contract Bandwidth Pool can be deleted for a given Vif. 
        /// The Contract Bandwidth Pool can be deleted if the only Vif using the pool 
        /// is the Vif given in the arguments.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private async Task<bool> CheckDeleteContractBandwidthPoolAsync(Vif vif)
        {
            var vifs = await GetAllByAttachmentIDAsync(vif.AttachmentID);
            return vifs.Where(q => q.ContractBandwidthPoolID == vif.ContractBandwidthPoolID).Count() == 1;
        }
    }
}