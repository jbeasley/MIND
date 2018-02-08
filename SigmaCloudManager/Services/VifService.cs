using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Models.NetModels.AttachmentNetModels;
using SCM.Models.SerializableModels.SerializableAttachmentModels;
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
            IContractBandwidthPoolService contractBandwidthPoolService, 
            INetworkSyncService netSync) : base(unitOfWork, mapper, netSync)
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

            using (var transaction = UnitOfWork.AttachmentRepository.context.Database.BeginTransaction())
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
                    if (contractBandwidthPool.TrustReceivedCosDscp != update.TrustReceivedCosDscp)
                    {
                        contractBandwidthPool.TrustReceivedCosDscp = update.TrustReceivedCosDscp;
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
        /// Check network sync of a Vif
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CheckNetworkSyncAsync(Vif vif)
        {
            var result = await CheckNetworkSyncHelperAsync(vif);
            await UpdateHelperAsync(vif);
            return result;
        }

        /// <summary>
        /// Perform a full sync check for a collection of Vifs
        /// </summary>
        /// <param name="vifs"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> CheckNetworkSyncAsync(IEnumerable<Vif> vifs, IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from vif in vifs select CheckNetworkSyncHelperAsync(vif)).ToList();
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

            foreach (var vif in vifs)
            {
                await UpdateHelperAsync(vif);
            }

            return results;
        }

        /// <summary>
        /// Sync a Vif to the network.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        public async Task<ServiceResult> SyncToNetworkAsync(Vif vif)
        {
            var result = await SyncToNetworkHelperAsync(vif);
            this.UnitOfWork.AttachmentRepository.Update(vif.Attachment);
            await UpdateHelperAsync(vif);
            return result;
        }

        /// <summary>
        /// Sync a collection of Vifs with the network
        /// </summary>
        /// <param name="vifs"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> SyncToNetworkAsync(IEnumerable<Vif> vifs, IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from vif in vifs select SyncToNetworkHelperAsync(vif)).ToList();
            var results = new List<ServiceResult>();

            while (tasks.Count() > 0)
            {
                Task<ServiceResult> task = await Task.WhenAny(tasks);
                await task;
                results.Add(task.Result);
                tasks.Remove(task);

                // Update caller with progress

                progress.Report(task.Result);
            }

            await Task.WhenAll(tasks);

            // Save updates to each Vif to the DB

            foreach (var vif in vifs)
            {
                await UpdateHelperAsync(vif);
            }

            return results;
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

            using (var transaction = UnitOfWork.VifRepository.context.Database.BeginTransaction())
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
        /// Delete a Vif from the network only.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteFromNetworkAsync(Vif vif)
        {
            var attachment = await AttachmentService.GetByIDAsync(vif.AttachmentID);
            var attachmentServiceData = Mapper.Map<AttachmentServiceNetModel>(attachment);
            var vifServiceData = Mapper.Map<AttachmentServiceNetModel>(vif);

            var taskResults = new List<NetworkSyncServiceResult>();
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            // Check the Attachment which the Vif belongs to and process accordingly
            // The resources we need to delete vary by attachment type (bundle, multiport etc) in accordance
            // with the NSO service model definition

            // If the Contract Bandwidth Pool is referenced only by the Vif we're about 
            // to delete then the Contract Bandwidth Pool is not shared by any other Vif and can be deleted

            var contractBandwidthPoolCanBeDeleted = vif.ContractBandwidthPool.Vifs.Count() == 1;
            if (vif.Attachment.IsBundle)
            {
                // Delete the Vif

                var vifResource = $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-bundle-interface/"
                    + $"{vif.Attachment.ID}";

                taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{vifResource}/vif/{vif.VlanTag}"));

                // If the Contract Bandwidth Pool is referenced only by the Vif we're about 
                // to delete then the Contract Bandwidth Pool is not shared by any other Vif and can be deleted

                if (contractBandwidthPoolCanBeDeleted)
                {
                    // Delete the Contract Bandwidth Pool resource

                    taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{vifResource}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));
                }
            }
            else if (vif.Attachment.IsMultiPort)
            {
                var attachmentResource = $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-multiport/{vif.Attachment.ID}";
                var data = vifServiceData.TaggedAttachmentMultiPorts.Single();

                foreach (var port in data.MultiPortMembers)
                {
                    var vifResource = $"{attachmentResource}/multiport-member/{port.InterfaceType},"
                        + $"{port.InterfaceName.Replace("/", "%2F")}";

                    // Delete vif from each member port

                    taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{vifResource}/vif/{vif.VlanTag}"));

                    // Check to delete the policy bandwidth

                    if (contractBandwidthPoolCanBeDeleted)
                    {

                        var policyBandwidthName = vifServiceData.TaggedAttachmentMultiPorts
                            .Single()
                            .MultiPortMembers
                            .Single(q => q.InterfaceType == port.InterfaceType && q.InterfaceName == port.InterfaceName)
                            .PolicyBandwidths
                            .Single()
                            .Name;

                        // Delete the policy bandwidth

                        taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{vifResource}/policy-bandwidth/{policyBandwidthName}"));
                    }
                }

                if (contractBandwidthPoolCanBeDeleted)
                {
                    // Delete the Contract Bandwidth Pool resource

                    taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{attachmentResource}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));
                }
            }
            else
            {
                // Delete the vif

                var port = vif.Attachment.Interfaces.Single().Ports.Single();
                var resource = $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-interface/"
                      + $"{port.Type},{port.Name.Replace("/", "%2F")}";

                taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{resource}/vif/{vif.VlanTag}"));

                // If the Contract Bandwidth Pool is referenced only by the vif we're about 
                // to delete then the Contract Bandwidth Pool is not shared by any other vif and can be deleted

                if (contractBandwidthPoolCanBeDeleted)
                {
                    // Delete the Contract Bandwidth Pool resource

                    taskResults.Add(await NetSync.DeleteFromNetworkAsync($"{resource}/contract-bandwidth-pool/"
                        + $"{vif.ContractBandwidthPool.Name}"));
                }
            }

            // Delete the vrf

            taskResults.Add(await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{vif.Attachment.Device.Name }/vrf/"
                + $"{vif.RoutingInstance.Name}"));


            // The VIF, associated Attacment, and associated Device, now require
            // re-sync with the network

            vif.RequiresSync = vif.VifRole.RequireSyncToNetwork;
            vif.ShowRequiresSyncAlert = vif.VifRole.RequireSyncToNetwork;
            vif.Attachment.RequiresSync = vif.Attachment.AttachmentRole.RequireSyncToNetwork; 
            vif.Attachment.ShowRequiresSyncAlert = vif.Attachment.AttachmentRole.RequireSyncToNetwork;
            vif.Attachment.Device.RequiresSync = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;
            vif.Attachment.Device.ShowRequiresSyncAlert = vif.Attachment.Device.DeviceRole.RequireSyncToNetwork;

            this.UnitOfWork.AttachmentRepository.Update(vif.Attachment);
            this.UnitOfWork.DeviceRepository.Update(vif.Attachment.Device);

            await UpdateHelperAsync(vif);

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
        /// Helper to check network sync for a Vif which is associated with an Attachment.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private  async Task<NetworkSyncServiceResult> CheckNetworkSyncAttachmentVifAsync(Vif vif)
        {
            var result = new NetworkSyncServiceResult
            {
                IsSuccess = true
            };

            var tasks = new List<Task<NetworkSyncServiceResult>>();

            var vifServiceModelData = Mapper.Map<AttachmentVifServiceNetModel>(vif.Vlans.Single());
            var serializableVifServiceModel = Mapper.Map<SerializableAttachmentVifService>(vifServiceModelData);
            if (vif.Attachment.IsBundle)
            {
                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableVifServiceModel,
                    $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-bundle-interface/{vif.Attachment.ID}/vif/{vif.VlanTag}"));

                var contractBandwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBandwidthPoolServiceModelData);
                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-bundle-interface/{vif.Attachment.ID}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));
            }

            else
            {
                var port = vif.Attachment.Interfaces.Single().Ports.Single();
                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableVifServiceModel,
                    $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-interface/{port.Type},"
                    + $"{port.Name.Replace("/", "%2F")}/vif/{vif.VlanTag}"));

                var contractBandwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBandwidthPoolServiceModelData);
                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                    + $"/tagged-attachment-interface/{port.Type},"
                    + $"{port.Name.Replace("/", "%2F")}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));
            }

            await Task.WhenAll(tasks);
            result.IsSuccess = tasks.Where(t => t.Result.IsSuccess).Count() == tasks.Count();

            return result;
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

        /// <summary>
        /// Helper to check network sync for a Vif which is associated with a Multi-Port Attachment.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private async Task<NetworkSyncServiceResult> CheckNetworkSyncMultiPortVifAsync(Vif vif)
        {
            var result = new NetworkSyncServiceResult { IsSuccess = true };
            var tasks = new List<Task<NetworkSyncServiceResult>>();

            foreach (var vlan in vif.Vlans)
            {
                // Create async task to check each vlan 

                var vifServiceModelData = Mapper.Map<MultiPortVifServiceNetModel>(vlan);
                var serializableVifServiceModel = Mapper.Map<SerializableMultiPortVifService>(vifServiceModelData);
                var port = vlan.Interface.Ports.Single();

                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableVifServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}" 
                    + $"/tagged-attachment-multiport/{vif.Attachment.ID}"
                    + $"/multiport-member/{port.Type},"
                    + $"{port.Name.Replace("/", "%2F")}/vif/{vif.VlanTag}"));

                var contractBandwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBandwidthPoolServiceModelData);
                tasks.Add(NetSync.CheckNetworkSyncAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-multiport/{vif.Attachment.ID}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));
            }

            // Await for all network checks to complete

            await Task.WhenAll(tasks);
            result.IsSuccess = tasks.Where(t => t.Result.IsSuccess).Count() == tasks.Count();

            return result;
        }

        /// <summary>
        /// Helper to sync a Vif with the network
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private async Task<ServiceResult> SyncToNetworkHelperAsync(Vif vif)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = vif,
                Context = vif.Attachment
            };

            // The parent Attachment must exist in the network before the VIF can be synchronised

            if (vif.Attachment.RequiresSync && vif.Created)
            {
                throw new UnstartableServiceException($"Unable to sync to network. Attachment {vif.Attachment.Name} must be synchronised first.");
            }

            // Sync VRF to the network

            var vrfServiceNetModel = Mapper.Map<RoutingInstanceServiceNetModel>(vif);
            var serializableRoutingInstanceServiceModel = Mapper.Map<SerializableRoutingInstanceService>(vrfServiceNetModel);

            await NetSync.SyncNetworkAsync(serializableRoutingInstanceServiceModel,
                $"/attachment/pe/{vif.Attachment.Device.Name}/vrf/{vif.RoutingInstance.Name}");

            // Create tasks to sync the VIF to the network

            var tasks = new List<Task<NetworkSyncServiceResult>>();

            if (vif.Attachment.IsBundle)
            {
                // Sync Contract Bandwidth Pool

                var contractBandwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBandwidthPoolServiceModelData);
                tasks.Add(NetSync.SyncNetworkAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-bundle-interface/{vif.Attachment.ID}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));

                var vifServiceModelData = Mapper.Map<AttachmentVifServiceNetModel>(vif.Vlans.Single());
                var serializableVifServiceModel = Mapper.Map<SerializableAttachmentVifService>(vifServiceModelData);
                tasks.Add(NetSync.SyncNetworkAsync(serializableVifServiceModel,
                    $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-bundle-interface/{vif.Attachment.ID}/vif/{vif.VlanTag}"));
            }

            else if (vif.Attachment.IsMultiPort)
            {
                // Sync Contract Bandwidth Pool

                var contractBanwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBanwidthPoolServiceModelData);
                tasks.Add(NetSync.SyncNetworkAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-multiport/{vif.Attachment.ID}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));

                foreach (var vlan in vif.Vlans)
                {
                    // Sync Policy Bandwidth

                    var policyBandwidthServiceNetModelData = Mapper.Map<TaggedMultiPortPolicyBandwidthServiceNetModel>(vlan);
                    var serializablePolicyBandwidthServiceModel = Mapper.Map<SerializableTaggedMultiPortPolicyBandwidthService>(policyBandwidthServiceNetModelData);
                    var port = vlan.Interface.Ports.Single();

                    tasks.Add(NetSync.SyncNetworkAsync(serializablePolicyBandwidthServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-multiport/{vif.Attachment.ID}/multiport-member/{port.Type},"
                        + $"{port.Name.Replace("/", "%2F")}/policy-bandwidth/{serializablePolicyBandwidthServiceModel.Name}"));

                    // Sync each vlan 

                    var vifServiceModelData = Mapper.Map<MultiPortVifServiceNetModel>(vlan);
                    var serializableVifServiceModel = Mapper.Map<SerializableMultiPortVifService>(vifServiceModelData);
                    tasks.Add(NetSync.SyncNetworkAsync(serializableVifServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-multiport/{vif.Attachment.ID}"
                        + $"/multiport-member/{port.Type},"
                        + $"{port.Name.Replace("/", "%2F")}/vif/{vif.VlanTag}"));
                }
            }

            else
            {
                var port = vif.Attachment.Interfaces.Single().Ports.Single();
                var contractBanwidthPoolServiceModelData = Mapper.Map<ContractBandwidthPoolServiceNetModel>(vif.ContractBandwidthPool);
                var serializableContractBandwidthPoolServiceModel = Mapper.Map<SerializableContractBandwidthPoolService>(contractBanwidthPoolServiceModelData);

                // Sync Contract Bandwidth Pool

                tasks.Add(NetSync.SyncNetworkAsync(serializableContractBandwidthPoolServiceModel, $"/attachment/pe/{vif.Attachment.Device.Name}"
                        + $"/tagged-attachment-interface/{port.Type},"
                    + $"{port.Name.Replace("/", "%2F")}/contract-bandwidth-pool/{vif.ContractBandwidthPool.Name}"));

                var vifServiceModelData = Mapper.Map<AttachmentVifServiceNetModel>(vif.Vlans.Single());
                var serializableVifServiceModel = Mapper.Map<SerializableAttachmentVifService>(vifServiceModelData);

                tasks.Add(NetSync.SyncNetworkAsync(serializableVifServiceModel,
                    $"/attachment/pe/{vif.Attachment.Device.Name}/tagged-attachment-interface/{port.Type},"
                    + $"{port.Name.Replace("/", "%2F")}/vif/{vif.VlanTag}"));
            }

            while (tasks.Any())
            {
                Task<NetworkSyncServiceResult> task = await Task.WhenAny(tasks);
                await task;
                tasks.Remove(task);
            }

            // The Vif no longer requires sync

            vif.RequiresSync = false;
            vif.ShowRequiresSyncAlert = false;

            // The Vif and the associated Attachment are now operational on the network

            vif.Created = false;
            vif.ShowCreatedAlert = false;
            vif.Attachment.Created = false;
            vif.Attachment.ShowCreatedAlert = false;

            return result;
        }

        /// <summary>
        /// Helper to check sync of a Vif with the network.
        /// </summary>
        /// <param name="vif"></param>
        /// <returns></returns>
        private async Task<ServiceResult> CheckNetworkSyncHelperAsync(Vif vif)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = vif,
                Context = vif.Attachment
            };

            NetworkSyncServiceResult syncResult;
            if (vif.Attachment.IsMultiPort)
            {
                // Process for a Vif associated with a Multi-Port Attachment

                syncResult = await CheckNetworkSyncMultiPortVifAsync(vif);
            }
            else
            {
                // Process for a Vif associated with any other type of Attachment

                syncResult = await CheckNetworkSyncAttachmentVifAsync(vif);
            }

            vif.RequiresSync = !syncResult.IsSuccess;
            vif.ShowRequiresSyncAlert = !syncResult.IsSuccess;
            result.IsSuccess = syncResult.IsSuccess;
            if (!syncResult.IsSuccess)
            {
                result.Add($"'{vif.Name}' is not in-sync with the network.");
            }

            return result;
        }
    }
}