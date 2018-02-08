using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class AttachmentSetRoutingInstanceService : BaseService, IAttachmentSetRoutingInstanceService
    {
        public AttachmentSetRoutingInstanceService(IUnitOfWork unitOfWork, 
            IAttachmentSetService attachmentSetService) : base(unitOfWork)
        {
            AttachmentSetService = attachmentSetService;
        }

        private IAttachmentSetService AttachmentSetService { get; }
        private string Properties { get; } = "AttachmentSet.Tenant,"
               + "RoutingInstance.Device.Location.SubRegion.Region,"
               + "RoutingInstance.Device.Plane,"
               + "RoutingInstance.Tenant,"
               + "RoutingInstance.Attachments.ContractBandwidthPool.Tenant,"
               + "RoutingInstance.Attachments.Interfaces.Ports,"
               + "RoutingInstance.Vifs.ContractBandwidthPool.Tenant,"
               + "RoutingInstance.Vifs.Attachment.Interfaces.Ports";

        public async Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(includeProperties: p, 
                AsTrackable: false);
        }

        public async Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id,
               includeProperties: p, 
               AsTrackable: false);
        }

        public async Task<AttachmentSetRoutingInstance> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => q.AttachmentSetRoutingInstanceID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<AttachmentSetRoutingInstance> GetByAttachmenSetAndRoutingInstanceAsync(int attachmentSetID, int vrfID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == attachmentSetID && q.RoutingInstanceID == vrfID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<ServiceResult> AddAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Insert(attachmentSetRoutingInstance);

            // Update VPNs to which the Attachment Set is bound in order to indicate
            // resync of the VPNs with the network is required

            await UpdateVpnsAsync(attachmentSetRoutingInstance.AttachmentSetID);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Update(attachmentSetRoutingInstance);

            // Update VPNs to which the Attachment Set is bound in order to indicate
            // resync of the VPNs with the network is required

            await UpdateVpnsAsync(attachmentSetRoutingInstance.AttachmentSetID);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            var result = new ServiceResult();

            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Delete(attachmentSetRoutingInstance);

            // Update VPNs to which the Attachment Set is bound in order to indicate
            // resync of the VPNs with the network is required

            await UpdateVpnsAsync(attachmentSetRoutingInstance.AttachmentSetID);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Get a collection of VRFs which can be used to satisfy an Attachment Set VRF request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoutingInstance>> GetCandidateRoutingInstances(AttachmentSetRoutingInstanceRequest request)
        {
            var attachmentSet = await UnitOfWork.AttachmentSetRepository.GetByIDAsync(request.AttachmentSetID);

            var vrfs = await UnitOfWork.RoutingInstanceRepository.GetAsync(q => q.Device.LocationID == request.LocationID && 
                                                               q.TenantID == request.TenantID,
                                                               includeProperties:
                                                               "Device,Tenant", 
                                                               AsTrackable: false);

            // Filter vrfs by plane if plane is specified

            if (request.PlaneID != null)
            {
                vrfs = vrfs.Where(q => q.Device.PlaneID == request.PlaneID).ToList();
            }

            return vrfs;
        }

        /// <summary>
        /// Helper to update the RequiresSync property of all VPNs to which a given
        /// Attachment Set are bound. This must be done to indicate that the VPNs require re-sync with
        /// the network whenever a VRF is added, removed, or updated in an Attachment Set.
        /// </summary>
        /// <param name="attachmentSetID"></param>
        /// <returns></returns>
        private async Task UpdateVpnsAsync(int attachmentSetID)
        {
            // Get all VPNs associated with the given Attachment Set

            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                                    .Where(x => x.AttachmentSetID == attachmentSetID)
                                                                    .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}