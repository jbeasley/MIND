using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnAttachmentSetService : BaseService, IVpnAttachmentSetService
    {
        private readonly string _properties = "AttachmentSet.Tenant,"
       + "Vpn.VpnTopologyType,"
       + "Vpn.VpnTenancyType,"
       + "Vpn.MulticastVpnServiceType,"
       + "Vpn.MulticastVpnDirectionType,"
       + "AttachmentSet.MulticastVpnDomainType,"
       + "AttachmentSet.AttachmentRedundancy,"
       + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Tenant,"
       + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Vifs.Attachment.Interfaces.Ports,"
       + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Attachments.Interfaces.Ports";

        public VpnAttachmentSetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Return an attachment set association with a vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q => 
                    q.VpnAttachmentSetID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Return all vpn attachment set associations for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q =>
                    q.AttachmentSetID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }

        /// <summary>
        /// Return all vpn attachment set associations for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q =>
                    q.VpnID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }

        /// <summary>
        /// Add an attachment set to a vpn.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Insert(vpnAttachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update an attachment set association with a vpn
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Update(vpnAttachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Remove an attachment set from a vpn
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Delete(vpnAttachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}