using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using Mind.Builders;
using SCM.Validators;
using SCM.Services;

namespace Mind.Services
{
    public class VpnService : BaseService, IVpnService
    {
        private readonly Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector> _directorFactory;
        private readonly Func<Vpn, IVpnUpdateDirector> _updateDirectorFactory;

        public VpnService(IUnitOfWork unitOfWork, IMapper mapper, Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector> directorFactory,
            Func<Vpn, IVpnUpdateDirector> updateDirectorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
        }

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
        /// Return all vpns
        /// </summary>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllAsync(bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var orderBy = OrderBy(sortKey);
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                        AsTrackable: asTrackable,
                        orderBy: orderBy)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList();
        }

        /// <summary>
        /// Return a single vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<Vpn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.VpnRepository.GetAsync(
                q =>
                    q.VpnID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Return all vpns which are associated with a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByAttachmentSetIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var orderBy = OrderBy(sortKey);
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                    q =>
                        q.VpnAttachmentSets
                        .Where(r => r.AttachmentSetID == id)
                        .Any(),
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                        AsTrackable: asTrackable,
                        orderBy: orderBy)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList();
        }

        /// <summary>
        /// Return all vpns for a given tenant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var orderBy = OrderBy(sortKey);
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                q => 
                    q.TenantID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                    AsTrackable: false,
                    orderBy: orderBy)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList().GroupBy(q => q.VpnID).Select(r => r.First());
        }

        public async Task<Vpn> AddAsync(int tenantId, Mind.Models.RequestModels.VpnRequest request)
        {
            var director = _directorFactory(request);
            var vpn = await director.BuildAsync(tenantId, request);
            this.UnitOfWork.VpnRepository.Insert(vpn);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpn.VpnID, deep: true);
        }

        /// <summary>
        /// Update a vpn.
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Vpn> UpdateAsync(int vpnId, Mind.Models.RequestModels.VpnUpdate update)
        {
            var vpn = (from result in await UnitOfWork.VpnRepository.GetAsync(
                   q =>
                       q.VpnID == vpnId,
                       query: q => q.IncludeDeepProperties(),
                       AsTrackable: false)
                       select result)
                       .Single();

            var updateDirector = _updateDirectorFactory(vpn);
            await updateDirector.UpdateAsync(vpnId, update);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnId, deep: true);
        }

        /// <summary>
        /// Delete a vpn.
        /// </summary>
        /// <param name="vpnId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int vpnId)
        {
            var vpn = (from result in await UnitOfWork.VpnRepository.GetAsync(
                    q =>
                       q.VpnID == vpnId,
                       query: q => q.IncludeDeleteValidationProperties(),
                       AsTrackable: true)
                       select result)
                       .Single();

            vpn.ValidateDelete();
            this.UnitOfWork.VpnRepository.Delete(vpn);
            await this.UnitOfWork.SaveAsync();
        }
    }
}