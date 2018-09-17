using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SCM.Data;
using SCM.Models;
using SCM.Services;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    /// <summary>
    /// Base service logic for vifs
    /// </summary>
    public abstract class BaseVifService : BaseService
    {
        public BaseVifService(IUnitOfWork unitOfWork, IMapper mapper, IValidator validator) : base(unitOfWork, mapper, validator)
        {
        }

        public BaseVifService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Get a vif by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public virtual async Task<Vif> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VifRepository.GetAsync(
                q => 
                    q.VifID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.VifRole),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Get all vifs for a given attachment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Vif>> GetAllByAttachmentIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VifRepository.GetAsync(
                 q =>
                    q.AttachmentID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.VifRole),
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }
    }
}
