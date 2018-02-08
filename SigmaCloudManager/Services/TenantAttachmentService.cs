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
    /// Service logic for Attachments
    /// </summary>
    public class TenantAttachmentService : AttachmentService, ITenantAttachmentService
    {

        public TenantAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            INetworkSyncService netSync,
            IAttachmentFactory attachmentFactory,
            IRoutingInstanceFactory vrfFactory) : base(unitOfWork, mapper, netSync, attachmentFactory, vrfFactory)
        {
        }

        /// <summary>
        /// Return all Attachments for a given Tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Attachment>> GetAllByTenantIDAsync(int tenantID, bool? roleRequireSyncToNetwork = null,
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null, 
            bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "AttachmentRole";
            var query = from attachments in await UnitOfWork.AttachmentRepository.GetAsync(q => q.TenantID == tenantID
            && (q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleType.TenantFacing ||
            q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleType.TenantInfrastructure),
                includeProperties: p,
                AsTrackable: false)
                        select attachments;

            if (roleRequireSyncToNetwork != null)
            {
                query = query.Where(x => x.AttachmentRole.RequireSyncToNetwork);
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
        /// Return all Attachments for a given vpn.
        /// </summary>
        /// <param name="attachmentID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Attachment>> GetAllByVpnIDAsync(int vpnID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var result = await UnitOfWork.AttachmentRepository
                .GetAsync(q => q.RoutingInstance.AttachmentSetRoutingInstances
                .SelectMany(r => r.AttachmentSet.VpnAttachmentSets)
                .Where(s => s.VpnID == vpnID)
                .Any(),
                includeProperties: p,
                AsTrackable: false);

            return result.ToList();
        }

        /// <summary>
        /// Check network sync of an Attachment.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CheckNetworkSyncAsync(Attachment attachment)
        {
            var result = await CheckNetworkSyncHelperAsync(attachment);
            await base.UpdateAsync(attachment);
            return result;
        }

        /// <summary>
        /// Check network sync of a collection of Attachments
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> CheckNetworkSyncAsync(IEnumerable<Attachment> attachments,
            IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from attachment in attachments select CheckNetworkSyncHelperAsync(attachment)).ToList();
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

            foreach (var attachment in attachments)
            {
                foreach (var vif in attachment.Vifs)
                {
                    this.UnitOfWork.VifRepository.Update(vif);
                }
                this.UnitOfWork.AttachmentRepository.Update(attachment);
            }

            await this.UnitOfWork.SaveAsync();

            return results;
        }

        /// <summary>
        /// Sync an Attachment to the network
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<ServiceResult> SyncToNetworkAsync(Attachment attachment) {

            var result = await SyncToNetworkHelperAsync(attachment);

            foreach (var vif in attachment.Vifs)
            {
                this.UnitOfWork.VifRepository.Update(vif);
            }
            this.UnitOfWork.AttachmentRepository.Update(attachment);

            await this.UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Sync a collection of Attachments to the network.
        /// </summary>
        /// <param name="attachments"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> SyncToNetworkAsync(IEnumerable<Attachment> attachments,
            IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from attachment in attachments select SyncToNetworkHelperAsync(attachment)).ToList();
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

            // Save updates to each Attachment in the db

            foreach (var attachment in attachments)
            {
                await base.UpdateAsync(attachment);
            }

            return results;
        }

        /// <summary>
        /// Delete an Attachment from the network.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteFromNetworkAsync(Attachment attachment)
        {
            var networkSyncServiceResults = new List<NetworkSyncServiceResult>();
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            var taskResult = new NetworkSyncServiceResult();
            var serviceModelData = Mapper.Map<AttachmentServiceNetModel>(attachment);

            // Check what type of Attachment we have (e.g. tagged, bundle etc) and call appropriate 
            // network server-side resource to delete

            if (serviceModelData.TaggedAttachmentBundleInterfaces.Any())
            {
                var data = serviceModelData.TaggedAttachmentBundleInterfaces.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                    + $"/tagged-attachment-bundle-interface/{data.BundleID}");
            }
            else if (serviceModelData.TaggedAttachmentInterfaces.Any())
            {
                var data = serviceModelData.TaggedAttachmentInterfaces.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                   + $"/tagged-attachment-interface/{data.InterfaceType},"
                   + data.InterfaceName.Replace("/", "%2F"));
            }
            else if (serviceModelData.TaggedAttachmentMultiPorts.Any())
            {
                var data = serviceModelData.TaggedAttachmentMultiPorts.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                  + $"/tagged-attachment-multiport/{data.Name}");
            }
            else if (serviceModelData.UntaggedAttachmentBundleInterfaces.Any())
            {
                var data = serviceModelData.UntaggedAttachmentBundleInterfaces.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                   + $"/untagged-attachment-bundle-interface/{data.BundleID}");
            }
            else if (serviceModelData.UntaggedAttachmentInterfaces.Any())
            {
                var data = serviceModelData.UntaggedAttachmentInterfaces.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                   + $"/untagged-attachment-interface/{data.InterfaceType},"
                   + data.InterfaceName.Replace("/", "%2F"));
            }
            else if (serviceModelData.UntaggedAttachmentMultiPorts.Any())
            {
                var data = serviceModelData.UntaggedAttachmentMultiPorts.Single();
                taskResult = await NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                    + $"/untagged-attachment-multiport/{data.Name}");
            }

            result.NetworkSyncServiceResults.Add(taskResult);
           
            // Check for VRFs to delete from network
            if (serviceModelData.RoutingInstances.Any())
            {

                var vrfTasks = (from vrf
                                in serviceModelData.RoutingInstances
                                select NetSync.DeleteFromNetworkAsync($"/attachment/pe/{attachment.Device.Name}"
                                + $"/vrf/{vrf.RoutingInstanceName}")).ToList();

                while (vrfTasks.Count() > 0)
                {
                    Task<NetworkSyncServiceResult> task = await Task.WhenAny(vrfTasks);
                    result.NetworkSyncServiceResults.Add(task.Result);
                    vrfTasks.Remove(task);
                }
            }

            foreach (var vif in attachment.Vifs)
            {
                vif.RequiresSync = vif.VifRole.RequireSyncToNetwork;
                vif.ShowRequiresSyncAlert = vif.VifRole.RequireSyncToNetwork;
                this.UnitOfWork.VifRepository.Update(vif);
            }

            attachment.RequiresSync = attachment.AttachmentRole.RequireSyncToNetwork;
            attachment.ShowRequiresSyncAlert = attachment.AttachmentRole.RequireSyncToNetwork;
            this.UnitOfWork.AttachmentRepository.Update(attachment);

            attachment.Device.RequiresSync = attachment.Device.DeviceRole.RequireSyncToNetwork;
            attachment.Device.ShowRequiresSyncAlert = attachment.Device.DeviceRole.RequireSyncToNetwork;

            this.UnitOfWork.DeviceRepository.Update(attachment.Device);

            await this.UnitOfWork.SaveAsync();

            return result;
        }
     
        /// <summary>
        /// Helper to sync an Attachment with the network.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private async Task<ServiceResult> SyncToNetworkHelperAsync(Attachment attachment)
        {
            var networkSyncServiceResults = new List<NetworkSyncServiceResult>();
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = attachment,
                Context = attachment.Tenant
            };

            var vrfs = new List<SerializableRoutingInstanceService>();
            if (attachment.RoutingInstance != null)
            {
                var attachmentRoutingInstance = Mapper.Map<RoutingInstanceServiceNetModel>(attachment);
                var serializableAttachmentRoutingInstance = Mapper.Map<SerializableRoutingInstanceService>(attachmentRoutingInstance);
                vrfs.Add(serializableAttachmentRoutingInstance);
            }

            if (attachment.Vifs.Any())
            {
                var vifRoutingInstances = Mapper.Map<List<RoutingInstanceServiceNetModel>>(attachment.Vifs);
                var serializableVifRoutingInstances = Mapper.Map<List<SerializableRoutingInstanceService>>(vifRoutingInstances);
                vrfs.AddRange(serializableVifRoutingInstances);
            }

            if (vrfs.Any())
            {
                var vrfTasks = (from serializableRoutingInstance
                                in vrfs
                                select NetSync.SyncNetworkAsync(serializableRoutingInstance, 
                                $"/attachment/pe/{attachment.Device.Name}/vrf/{serializableRoutingInstance.RoutingInstanceName}")).ToList();

                while (vrfTasks.Any())
                {
                    Task<NetworkSyncServiceResult> task = await Task.WhenAny(vrfTasks);
                    vrfTasks.Remove(task);
                }
            }

            // Check what type of Attachment we have and call the network server 
            // to update the appropriate resource

            if (attachment.IsTagged)
            {
                if (attachment.IsBundle)
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentBundleInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentBundleInterfaceService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-bundle-interface/{serializableAttachmentServiceModel.BundleID}");
                }

                else if (attachment.IsMultiPort)
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentMultiPortServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentMultiPortService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                       $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-multiport/{serializableAttachmentServiceModel.Name}");
                }
                else
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentInterfaceService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-interface/{serializableAttachmentServiceModel.InterfaceType},"
                        + serializableAttachmentServiceModel.InterfaceName.Replace("/", "%2F"));
                }
            }
            else
            {
                if (attachment.IsBundle)
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentBundleInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentBundleInterfaceService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                            $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-bundle-interface/{serializableAttachmentServiceModel.BundleID}");
                }
                else if (attachment.IsMultiPort)
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentMultiPortServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentMultiPortService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-multiport/{serializableAttachmentServiceModel.Name}");
                }
                else
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentInterfaceService>(attachmentServiceNetModel);
                    await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-interface/{serializableAttachmentServiceModel.InterfaceType},"
                        + serializableAttachmentServiceModel.InterfaceName.Replace("/", "%2F"));
                }
            }

            // Attachment and any associated VIFs no longer requires sync so set 
            // RequiresSync property to false
            // Attachment and any associated VIFs are now operation on the network
            // so set Created property to false

            attachment.RequiresSync = false;
            attachment.Created = false;

            foreach (var vif in attachment.Vifs)
            {
                vif.RequiresSync = false;
                vif.Created = false;
            }

            return result;
        }

        /// <summary>
        /// Helper to check network sync of an Attachment.
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private async Task<ServiceResult> CheckNetworkSyncHelperAsync(Attachment attachment)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = attachment,
                Context = attachment.Tenant
            };

            NetworkSyncServiceResult syncResult;
            if (attachment.IsTagged)
            {
                if (attachment.IsBundle)
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentBundleInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentBundleInterfaceService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-bundle-interface/{serializableAttachmentServiceModel.BundleID}");
                }

                else if (attachment.IsMultiPort)
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentMultiPortServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentMultiPortService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                       $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-multiport/{serializableAttachmentServiceModel.Name}");
                }
                else
                {
                    var attachmentServiceNetModel = Mapper.Map<TaggedAttachmentInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableTaggedAttachmentInterfaceService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/tagged-attachment-interface/{serializableAttachmentServiceModel.InterfaceType},"
                        + serializableAttachmentServiceModel.InterfaceName.Replace("/", "%2F"));
                }
            }
            else
            {
                if (attachment.IsBundle)
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentBundleInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentBundleInterfaceService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                            $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-bundle-interface/{serializableAttachmentServiceModel.BundleID}");
                }
                else if (attachment.IsMultiPort)
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentMultiPortServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentMultiPortService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-multiport/{serializableAttachmentServiceModel.Name}");
                }
                else
                {
                    var attachmentServiceNetModel = Mapper.Map<UntaggedAttachmentInterfaceServiceNetModel>(attachment);
                    var serializableAttachmentServiceModel = Mapper.Map<SerializableUntaggedAttachmentInterfaceService>(attachmentServiceNetModel);
                    syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel,
                        $"/attachment/pe/{attachment.Device.Name}/untagged-attachment-interface/{serializableAttachmentServiceModel.InterfaceType},"
                        + serializableAttachmentServiceModel.InterfaceName.Replace("/", "%2F"));
                }
            }

            attachment.RequiresSync = !syncResult.IsSuccess;
            attachment.ShowRequiresSyncAlert = !syncResult.IsSuccess;
            result.IsSuccess = syncResult.IsSuccess;
            if (!syncResult.IsSuccess)
            {
                result.Add($"'{attachment.Name}' is not in-sync with the network.");
            }

            return result;
        }
    }
}