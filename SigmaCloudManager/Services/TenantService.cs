using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Microsoft.EntityFrameworkCore;
using SCM.Validators;
using SCM.Services;

namespace Mind.Services
{
    public class TenantService : BaseService, ITenantService
    {
        private readonly ITenantValidator _validator;
        private string _properties = "TenantIpNetworks," +
            "TenantCommunities," +
            "TenantMulticastGroups," +
            "Devices," +
            "Attachments.Interfaces.Ports," +
            "Attachments.Device," +
            "Attachments.ContractBandwidthPool," +
            "Vifs.ContractBandwidthPool," +
            "Vifs.Attachment.Interfaces.Ports," +
            "Vifs.Attachment.Device," +
            "Vifs.Attachment.ContractBandwidthPool";

        public TenantService(IUnitOfWork unitOfWork, ITenantValidator validator) : base(unitOfWork, validator)
        {
            _validator = validator;
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync(bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.TenantRepository.GetAsync(includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty, 
                AsTrackable: asTrackable);
        }

        public async Task<Tenant> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantRepository.GetAsync(q => q.TenantID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }
    
        public async Task<Tenant> GetByNameAsync(string name, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.TenantRepository.GetAsync(q => q.Name == name,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<Tenant> AddAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Insert(tenant);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenant.TenantID, deep: true, asTrackable: false);
        }
 
        public async Task<Tenant> UpdateAsync(Tenant tenant)
        {
            this.UnitOfWork.TenantRepository.Update(tenant);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(tenant.TenantID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int tenantId)
        {
            await _validator.ValidateDeleteAsync(tenantId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            await this.UnitOfWork.TenantRepository.DeleteAsync(tenantId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
