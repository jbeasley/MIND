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
using SCM.Validators;
using Mind.Builders;

namespace SCM.Services
{
    /// <summary>
    /// Service logic for Attachments
    /// </summary>
    public class TenantAttachmentService : AttachmentService, ITenantAttachmentService
    {

        public TenantAttachmentService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentFactory factory,
            IRoutingInstanceFactory routingInstanceFactory) : base(unitOfWork, mapper, factory, routingInstanceFactory)
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
    }
}