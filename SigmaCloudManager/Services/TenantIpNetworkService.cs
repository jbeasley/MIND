using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Validators;
using SCM.Services;
using System.Net;

namespace Mind.Services
{
    public class TenantIpNetworkService : BaseService, ITenantIpNetworkService
    {
        private readonly string _properties = "Tenant,"
                + "VpnTenantIpNetworksIn.AttachmentSet,"
                + "VpnTenantIpNetworksOut.AttachmentSet";

        private readonly ITenantIpNetworkValidator _validator;

        public TenantIpNetworkService(IUnitOfWork unitOfWork, ITenantIpNetworkValidator validator) : base(unitOfWork, validator)
        {
            _validator = validator;
        }

        public async Task<IEnumerable<TenantIpNetwork>> GetAllByTenantIDAsync(int id, string searchString = "", bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await this.UnitOfWork.TenantIpNetworkRepository.GetAsync(q => q.TenantID == id,
                     includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                     AsTrackable: asTrackable)
                         select result);

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(q => q.CidrName == searchString);
            return query.ToList();
        }

        public async Task<TenantIpNetwork> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantIpNetworkRepository.GetAsync(q => q.TenantIpNetworkID == id,
                   includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                   AsTrackable: asTrackable)
                   select result)
                   .SingleOrDefault();
        }
        public async Task<TenantIpNetwork> AddAsync(TenantIpNetwork tenantIpNetwork)
        {
            // Normalise the IP Prefix according to the Cidr length.
            // e.g. - 10.1.1.0/16 becomes 10.1.0.0/16

            var network = IPNetwork.Parse($"{tenantIpNetwork.Ipv4Prefix}/{tenantIpNetwork.Ipv4Length}");
            tenantIpNetwork.Ipv4Prefix = network.Network.ToString();

            this.UnitOfWork.TenantIpNetworkRepository.Insert(tenantIpNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetwork.TenantIpNetworkID, deep: true);
        }

        public async Task DeleteAsync(int tenantNetworkId)
        {
            await _validator.ValidateDeleteAsync(tenantNetworkId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            await this.UnitOfWork.TenantIpNetworkRepository.DeleteAsync(tenantNetworkId);
            await this.UnitOfWork.SaveAsync();
        }

        public async Task<TenantIpNetwork> UpdateAsync(TenantIpNetwork tenantIpNetwork)
        {
            // Normalise the IP Prefix according to the Cidr length.
            // e.g. - 10.1.1.0/16 becomes 10.1.0.0/16

            var network = IPNetwork.Parse($"{tenantIpNetwork.Ipv4Prefix}/{tenantIpNetwork.Ipv4Length}");
            tenantIpNetwork.Ipv4Prefix = network.Network.ToString();

            await _validator.ValidateChangesAsync(tenantIpNetwork);
            if (!_validator.IsValid) throw new ServiceValidationException();

            this.UnitOfWork.TenantIpNetworkRepository.Update(tenantIpNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(tenantIpNetwork.TenantIpNetworkID, deep: true);
        }
    }
}
