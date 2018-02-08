using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class AddressFamilyService : BaseService, IAddressFamilyService
    {
        public AddressFamilyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<AddressFamily>> GetAllAsync()
        {
            return await this.UnitOfWork.AddressFamilyRepository.GetAsync(AsTrackable: false);
        }

        public async Task<AddressFamily> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.AddressFamilyRepository.GetAsync(q => q.AddressFamilyID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(AddressFamily addressFamily)
        {
            this.UnitOfWork.AddressFamilyRepository.Insert(addressFamily);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(AddressFamily addressFamily)
        {
            this.UnitOfWork.AddressFamilyRepository.Update(addressFamily);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(AddressFamily addressFamily)
        {
            this.UnitOfWork.AddressFamilyRepository.Delete(addressFamily);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
